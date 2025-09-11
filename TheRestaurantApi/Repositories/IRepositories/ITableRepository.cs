using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Repositories.IRepositories;

public interface ITableRepository
{ 
    Task<List<Table>> GetTablesAsync();

    Task<Table?> GetTableByTableNumberAsync(int tableNumber);

    Task<Unit> CreateTableAsync(Table table);

    Task<Unit> UpdateTableAsync(Table table);

    Task<Unit> DeleteTableAsync(Table table);
}

