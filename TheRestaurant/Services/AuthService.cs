using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TheRestaurant.Data;
using TheRestaurant.DTOs;
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

            return ServiceResponse<AuthResponseDto>.Success(
                HttpStatusCode.OK,
                new AuthResponseDto(accessToken),
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

    private string CreateJwt(Models.User user)
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
}