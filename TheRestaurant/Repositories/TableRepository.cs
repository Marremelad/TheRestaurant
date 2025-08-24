using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class TableRepository(TheRestaurantDbContext context) : ITableRepository
{
    public async Task<IEnumerable<Table>> GetTablesAsync() => 
        await context.Tables.ToListAsync();

    public async Task<Table?> GetTableAsync(int tableNumber) =>
        await context.Tables.FirstOrDefaultAsync(table => table.Number == tableNumber);

    public async Task<Unit> CreateTableAsync(Table table)
    {
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}