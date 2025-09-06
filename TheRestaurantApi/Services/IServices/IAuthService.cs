using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IAuthService
{
    Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);

    Task<ServiceResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken);

    Task<ServiceResponse<Unit>> RevokeRefreshTokenAsync(string refreshToken);
}