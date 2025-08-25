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
    public async Task<ServiceResponse<IEnumerable<TableDto>>> GetTablesAsync()
    {
        try
        {
            var tables = await repository.GetTablesAsync();
            
            return ServiceResponse<IEnumerable<TableDto>>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDtos(tables),
                "Tables fetched successfully."
                );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching tables.");
            return ServiceResponse<IEnumerable<TableDto>>.Failure(
                HttpStatusCode.InternalServerError,
                "An error occurred while fetching tables."
                );
        }
    }

    public async Task<ServiceResponse<TableDto?>> GetTableAsync(int tableNumber)
    {
        try
        {
            var table = await repository.GetTableAsync(tableNumber);
            
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
            logger.LogError(ex,"An error occurred while fetching table number ({TableNumber}).", tableNumber);
            return ServiceResponse<TableDto?>.Failure(
                HttpStatusCode.InternalServerError,
                $"An error occurred while fetching table number ({tableNumber})."
                );
        }
    }

    public async Task<ServiceResponse<Unit>> CreateTableAsync(TableDto tableDto)
    {
        try
        {
            if (!await IsUniqueEntity(tableDto.Number))
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
            logger.LogError(ex, "An error occurred while trying to create a new table.");
            return ServiceResponse<Unit>.Failure(
                HttpStatusCode.InternalServerError,
                "An error occurred while trying to create a new table."
                );
        }
    }

    public async Task<bool> IsUniqueEntity(int primaryKey) =>
        await repository.GetTableAsync(primaryKey) == null;
}