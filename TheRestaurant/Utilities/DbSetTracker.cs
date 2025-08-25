using Microsoft.EntityFrameworkCore;
using TheRestaurant.Data;
using TheRestaurant.Models;

namespace TheRestaurant.Utilities;

public class DbSetTracker(TheRestaurantDbContext context)
{
    private IEnumerable<Table> Tables { get; set; } = [];

    public async Task LoadDataAsync()
    {
        Tables = await context.Tables.ToListAsync();
    }

    public int GetTablesCount() =>
        Tables.Count();
    
    public bool TableExists(int tableNumber) =>
        Tables.Any(table => table.Number == tableNumber);
}