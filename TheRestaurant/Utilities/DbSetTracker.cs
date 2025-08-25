using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;

namespace TheRestaurant.Utilities;

public class DbSetTracker(TheRestaurantDbContext context)
{
    public async Task<int> GetTablesCount() =>
        await context.Tables.CountAsync();

    public async Task<bool> TableExists(int primaryKey) =>
        await context.Tables.AnyAsync(table => table.Number == primaryKey);
}