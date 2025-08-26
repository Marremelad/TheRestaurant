using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;

namespace TheRestaurant.Utilities;

public class DbSetTracker(ITableRepository tableRepository)
{
    public IEnumerable<Table> Tables { get; private set; } = [];

    public async Task LoadDataAsync()
    {
        Tables = await tableRepository.GetTablesAsync();
    }

    public int GetTablesCount() =>
        Tables.Count();
    
    public bool TableExists(int tableNumber) =>
        Tables.Any(table => table.Number == tableNumber);
}