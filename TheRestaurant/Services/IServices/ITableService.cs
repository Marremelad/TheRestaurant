using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Services.IServices;

public interface ITableService
{
    Task<IEnumerable<TableDto>> GetTablesAsync();

    Task<Table?> GetTableAsync(int tableId);

    Task<int> CreateTableAsync(Table newTable);
}