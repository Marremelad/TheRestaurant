using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories;

public class TableRepository(TheRestaurantApiDbContext context) : ITableRepository
{
    public async Task<List<Table>> GetTablesAsync() => 
        await context.Tables.ToListAsync();

    public async Task<Table?> GetTableByTableNumberAsync(int tableNumber) =>
        await context.Tables.FirstOrDefaultAsync(table => table.Number == tableNumber);

    public async Task<Unit> CreateTableAsync(Table table)
    {
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        return Unit.Value;
    }

    public async Task<Unit> DeleteTableAsync(Table table)
    {
        context.Tables.Remove(table);
        await context.SaveChangesAsync();
        return Unit.Value;
    }
}