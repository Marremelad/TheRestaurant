using TheRestaurant.DTOs;
using TheRestaurant.Models;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface ITableService
{
    Task<ServiceResponse<IEnumerable<TableDto>>> GetTablesAsync();

    Task<ServiceResponse<TableDto?>> GetTableAsync(int tableId);

    Task<int> CreateTableAsync(Table newTable);
}