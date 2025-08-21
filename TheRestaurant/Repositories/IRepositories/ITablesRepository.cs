using TheRestaurant.Models;

namespace TheRestaurant.Repositories.IRepositories;

public interface ITablesRepository
{ 
    Task<IEnumerable<Table>> GetTablesAsync();

    Task<Table> GetTableAsync(int tableId);

    Task<int> CreateTableAsync(Table newTable);
}

