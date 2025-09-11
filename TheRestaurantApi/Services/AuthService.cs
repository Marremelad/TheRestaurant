using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheRestaurant.DTOs;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Security;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

/// <summary>
/// Handles authentication operations including login, token generation, and refresh token management.
/// </summary>
public class AuthService(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    JwtSettings jwtSettings,
    ILogger<AuthService> logger) : IAuthService
{
    /// <summary>
    /// Authenticates a user with username/password and returns JWT access token and refresh token.
    /// </summary>
    public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        return await loginDto.ValidateAndExecuteAsync(async () =>
        {
            try
            {
                // Retrieve user from database by username.
                var user = await userRepository.GetUserByUserNameAsync(loginDto.UserName);

                if (user == null)
                    return ServiceResponse<AuthResponseDto>.Failure(
                        HttpStatusCode.BadRequest,
                        "Invalid username or password."
                    );

                // Verify the provided password against the stored hash using BCrypt.
                var isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

                if (!isValidPassword)
                    return ServiceResponse<AuthResponseDto>.Failure(
                        HttpStatusCode.BadRequest,
                        "Invalid username or password."
                    );

                // Generate JWT access token and refresh token for authenticated user.
                var accessToken = CreateJwt(user);
                var refreshToken = await CreateRefreshTokenAsync(user.Id);

                return ServiceResponse<AuthResponseDto>.Success(
                    HttpStatusCode.OK,
                    new AuthResponseDto(accessToken, refreshToken.Token),
                    "Login successful."
                );
            }
            catch (Exception ex)
            {
                const string message = "An error occurred during login.";
                logger.LogError(ex, message);
                return ServiceResponse<AuthResponseDto>.Failure(
                    HttpStatusCode.InternalServerError,
                    $"{message}: {ex.Message}"
                );
            }
        });
    }

    /// <summary>
    /// Creates a JWT access token containing user claims and expiration time.
    /// </summary>
    private string CreateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

        // Define token claims including user ID, JTI (unique token ID), and username.
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

    /// <summary>
    /// Creates and stores a new refresh token with 7-day expiration for the specified user.
    /// </summary>
    private async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
    {
        var token = Guid.NewGuid().ToString();

        var refreshToken = new RefreshToken
        {
            Token = token,
            UserIdFk = userId,
            CreatedDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddDays(7), // Refresh tokens expire after 7 days.
            IsRevoked = false,
            IsUsed = false
        };

        await refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);

        return refreshToken;
    }

    /// <summary>
    /// Validates a refresh token and generates new access/refresh token pair if valid.
    /// </summary>
    public async Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var storedRefreshToken = await refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
            
            // Validate refresh token exists and meets all security requirements.
            if (storedRefreshToken == null || !ValidateRefreshToken(storedRefreshToken))
            {
                return ServiceResponse<AuthResponseDto>.Failure(
                    HttpStatusCode.Unauthorized,
                    "Invalid refresh token."
                );
            }

            // Mark the refresh token as used to prevent replay attacks.
            storedRefreshToken.IsUsed = true;
            await refreshTokenRepository.UpdateRefreshTokenAsync(storedRefreshToken);

            // Generate new token pair for the user.
            var user = storedRefreshToken.User!;
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = await CreateRefreshTokenAsync(user.Id);

            return ServiceResponse<AuthResponseDto>.Success(
                HttpStatusCode.OK,
                new AuthResponseDto(newAccessToken, newRefreshToken.Token),
                "Token refreshed successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred during token refresh.";
            logger.LogError(ex, message);
            return ServiceResponse<AuthResponseDto>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }
    
    /// <summary>
    /// Revokes a refresh token by marking it as revoked to prevent future use.
    /// </summary>
    public async Task<ServiceResponse<Unit>> RevokeRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var storedRefreshToken = await refreshTokenRepository.GetRefreshTokenAsync(refreshToken);

            if (storedRefreshToken == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    "Invalid refresh token."
                );

            storedRefreshToken.IsRevoked = true;
            await refreshTokenRepository.UpdateRefreshTokenAsync(storedRefreshToken);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                Unit.Value,
                "Refresh token was revoked successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to revoke refresh token.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    /// <summary>
    /// Validates refresh token by checking expiration, usage status, and revocation status.
    /// </summary>
    private static bool ValidateRefreshToken(RefreshToken token)
    {
        return token.ExpirationDate > DateTime.UtcNow && 
               token is { IsUsed: false, IsRevoked: false };
    }
}