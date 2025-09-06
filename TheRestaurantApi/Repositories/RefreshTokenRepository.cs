using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class RefreshTokenRepository(TheRestaurantApiDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken) =>
        await context.RefreshTokens.FirstOrDefaultAsync(storedToken => storedToken.Token == refreshToken);

    public async Task<Unit> UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        await context.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<Unit> CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}