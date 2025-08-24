using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface ITableService
{
    Task<ServiceResponse<IEnumerable<TableDto>>> GetTablesAsync();

    Task<ServiceResponse<TableDto?>> GetTableAsync(int tableId);

    Task<ServiceResponse<Unit>> CreateTableAsync(TableDto tableDto);
}