using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface ITableRepository
{ 
    Task<IEnumerable<Table>> GetTablesAsync();

    Task<Table?> GetTableAsync(int tableNumber);

    Task<Unit> CreateTableAsync(Table newTable);
}

