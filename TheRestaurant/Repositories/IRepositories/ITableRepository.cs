using TheRestaurant.Models;

namespace TheRestaurant.Repositories.IRepositories;

public interface ITableRepository
{ 
    Task<IEnumerable<Table>> GetTablesAsync();

    Task<Table?> GetTableAsync(int tableId);

    Task<int> CreateTableAsync(Table newTable);
}

