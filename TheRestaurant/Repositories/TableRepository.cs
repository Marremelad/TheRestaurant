using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Repositories;

public class TableRepository(TheRestaurantDbContext context) : ITableRepository
{
    public async Task<IEnumerable<Table>> GetTablesAsync() => 
        await context.Tables.ToListAsync();

    public async Task<Table?> GetTableAsync(int tableNumber) =>
        await context.Tables.FirstOrDefaultAsync(table => table.Number == tableNumber);

    public async Task<int> CreateTableAsync(Table newTable)
    {
        throw new NotImplementedException();
        // context.Tables.Add(newTable);
        // await context.SaveChangesAsync();
        // return newTable.Id;
    }
}