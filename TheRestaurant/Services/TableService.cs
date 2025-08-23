using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Models;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class TableService(
    ITableRepository repository,
    ILogger<TableService> logger
    ) : ITableService
{
    public async Task<ServiceResponse<IEnumerable<TableDto>>> GetTablesAsync()
    {
        try
        {
            return ServiceResponse<IEnumerable<TableDto>>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDtos(await repository.GetTablesAsync()),
                "Tables fetched successfully."
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An Error occured while fetching tables.");
            return ServiceResponse<IEnumerable<TableDto>>.Failure(
                HttpStatusCode.InternalServerError,
                "An error occurred while fetching tables.");
        }
    }

    public Task<Table?> GetTableAsync(int tableId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateTableAsync(Table newTable)
    {
        throw new NotImplementedException();
    }
}