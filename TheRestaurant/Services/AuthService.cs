using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TheRestaurant.Data;
using TheRestaurant.DTOs;
using TheRestaurant.Models;
using TheRestaurant.Security;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class AuthService(
    TheRestaurantDbContext context,
    JwtSettings jwtSettings,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null)
                return ServiceResponse<AuthResponseDto>.Failure(
                    HttpStatusCode.Unauthorized,
                    "Invalid username or password.");

            var isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isValidPassword)
                return ServiceResponse<AuthResponseDto>.Failure(
                    HttpStatusCode.Unauthorized,
                    "Invalid username or password.");

            var accessToken = CreateJwt(user);
            var refreshToken = await CreateRefreshTokenAsync(user.Id);

            return ServiceResponse<AuthResponseDto>.Success(
                HttpStatusCode.OK,
                new AuthResponseDto(accessToken, refreshToken.Token),
                "Login successful.");
        }
        catch (Exception ex)
        {
            const string message = "An error occurred during login.";
            logger.LogError(ex, message);
            return ServiceResponse<AuthResponseDto>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}");
        }
    }

    private string CreateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.UserName)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
    {
        var token = Guid.NewGuid().ToString();

        var refreshToken = new RefreshToken
        {
            Token = token,
            UserIdFk = userId,
            CreatedDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            IsUsed = false
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return refreshToken;
    }

    public async Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var storedRefreshToken = await context.RefreshTokens
                .Include(token => token.User)
                .FirstOrDefaultAsync(token => token.Token == refreshToken);

            if (storedRefreshToken == null || !ValidateRefreshToken(storedRefreshToken))
            {
                return ServiceResponse<AuthResponseDto>.Failure(
                    HttpStatusCode.Unauthorized,
                    "Invalid refresh token.");
            }

            storedRefreshToken.IsUsed = true;
            context.RefreshTokens.Update(storedRefreshToken);

            var user = storedRefreshToken.User!;
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = await CreateRefreshTokenAsync(user.Id);

            await context.SaveChangesAsync();

            return ServiceResponse<AuthResponseDto>.Success(
                HttpStatusCode.OK,
                new AuthResponseDto(newAccessToken, newRefreshToken.Token),
                "Token refreshed successfully.");
        }
        catch (Exception ex)
        {
            const string message = "An error occurred during token refresh.";
            logger.LogError(ex, message);
            return ServiceResponse<AuthResponseDto>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}");
        }
    }

    private static bool ValidateRefreshToken(RefreshToken token)
    {
        return token.ExpirationDate > DateTime.UtcNow && 
               token is { IsUsed: false, IsRevoked: false };
    }
}