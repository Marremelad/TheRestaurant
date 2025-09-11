using TheRestaurant.DTOs;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services.IServices;

public interface ITableService
{
    Task<ServiceResponse<List<TableDto>>> GetTablesAsync();

    Task<ServiceResponse<TableDto?>> GetTableByTableNumberAsync(int tableNumber);

    Task<ServiceResponse<Unit>> CreateTableAsync(TableDto tableDto);

    Task<ServiceResponse<Unit>> UpdateTableAsync(int number, TableUpdateDto dto);
    Task<ServiceResponse<Unit>> DeleteTableAsync(int tableNumber);
}