using System.Net;
using TheRestaurant.DTOs;
using TheRestaurant.Mappers;
using TheRestaurant.Repositories.IRepositories;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Services;

/// <summary>
/// Manages restaurant table configuration including creation, retrieval, and removal of seating arrangements.
/// </summary>
public class TableService(
    ITableRepository repository,
    ILogger<TableService> logger
    ) : ITableService
{
    /// <summary>
    /// Retrieves all configured tables in the restaurant for administrative management.
    /// </summary>
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

    /// <summary>
    /// Retrieves a specific table by its unique table number for detailed management operations.
    /// </summary>
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

    /// <summary>
    /// Creates a new table with specified number and capacity, ensuring no duplicate table numbers exist.
    /// </summary>
    public async Task<ServiceResponse<Unit>> CreateTableAsync(TableDto tableDto)
    {
        return await tableDto.ValidateAndExecuteAsync(async () =>
        {
            try
            {
                // Validate table number uniqueness to prevent duplicate table configurations.
                if (await repository.GetTableByTableNumberAsync(tableDto.Number) != null)
                    return ServiceResponse<Unit>.Failure(
                        HttpStatusCode.BadRequest,
                        $"A table with the assigned number ({tableDto.Number}) already exists."
                    );

                // Convert DTO to entity and persist to database.
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
        });
    }

    /// <summary>
    /// Removes a table from the restaurant configuration, including all associated reservation history.
    /// </summary>
    public async Task<ServiceResponse<Unit>> DeleteTableAsync(int tableNumber)
    {
        try
        {
            // Verify table exists before attempting deletion.
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