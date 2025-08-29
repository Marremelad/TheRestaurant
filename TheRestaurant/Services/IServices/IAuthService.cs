using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface IAuthService
{
    Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
}