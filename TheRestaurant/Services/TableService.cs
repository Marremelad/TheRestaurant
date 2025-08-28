using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

public class TableService(
    ITableRepository repository,
    ILogger<TableService> logger
    ) : ITableService
{
    public async Task<ServiceResponse<List<TableDto>>> GetTablesAsync()
    {
        try
        {
            var tables = await repository.GetTablesAsync();

            return ServiceResponse<List<TableDto>>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDtos(tables),
                "Tables fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch tables.";
            logger.LogError(ex, message);
            return ServiceResponse<List<TableDto>>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<TableDto?>> GetTableByTableNumberAsync(int tableNumber)
    {
        try
        {
            var table = await repository.GetTableByTableNumberAsync(tableNumber);

            if (table == null)
                return ServiceResponse<TableDto?>.Failure(
                    HttpStatusCode.NotFound,
                    $"Table number ({tableNumber}) does not exist."
                );

            return ServiceResponse<TableDto?>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDto(table),
                "Table fetched successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to fetch a table.";
            logger.LogError(ex, message);
            return ServiceResponse<TableDto?>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<Unit>> CreateTableAsync(TableDto tableDto)
    {
        try
        {
            if (await repository.GetTableByTableNumberAsync(tableDto.Number) != null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.BadRequest,
                    $"A table with the assigned number ({tableDto.Number}) already exists."
                );
            
            var table = TableMapper.ToEntity(tableDto);

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.Created,
                await repository.CreateTableAsync(table),
                "Table created successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to create a new table.";
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }

    public async Task<ServiceResponse<Unit>> DeleteTableAsync(int tableNumber)
    {
        try
        {
            var table = await repository.GetTableByTableNumberAsync(tableNumber);

            if (table == null)
                return ServiceResponse<Unit>.Failure(
                    HttpStatusCode.NotFound,
                    $"Table number ({tableNumber}) does not exist."
                );

            return ServiceResponse<Unit>.Success(
                HttpStatusCode.OK,
                await repository.DeleteTableAsync(table),
                $"Table number ({tableNumber}) was deleted successfully."
            );
        }
        catch (Exception ex)
        {
            const string message = "An error occurred while trying to delete a table." ;
            logger.LogError(ex, message);
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                $"{message}: {ex.Message}"
            );
        }
    }
}