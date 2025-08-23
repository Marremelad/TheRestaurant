using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;

namespace TheRestaurant.Services;

public class TableService(ITableRepository repository) : ITableService
{
    public async Task<IEnumerable<TableDto>> GetTablesAsync() =>
        TableMapper.ToDtos(await repository.GetTablesAsync());

    public Task<Table?> GetTableAsync(int tableId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateTableAsync(Table newTable)
    {
        throw new NotImplementedException();
    }
}