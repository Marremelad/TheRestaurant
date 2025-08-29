using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);

    Task<Unit> UpdateRefreshTokenAsync(RefreshToken refreshToken);
    
    Task<Unit> CreateRefreshTokenAsync(RefreshToken refreshToken);
}