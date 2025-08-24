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
            var tables = await repository.GetTablesAsync();
            
            return ServiceResponse<IEnumerable<TableDto>>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDtos(tables),
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

    public async Task<ServiceResponse<TableDto?>> GetTableAsync(int tableNumber)
    {
        try
        {
            var table = await repository.GetTableAsync(tableNumber);
            
            if (table == null)
                return ServiceResponse<TableDto?>.Failure(
                    HttpStatusCode.NotFound,
                    $"Table with id {tableNumber} does not exist."
                    ); 
            
            return ServiceResponse<TableDto?>.Success(
                HttpStatusCode.OK,
                TableMapper.ToDto(table),
                "Table fetched successfully."
                );
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"An error occurred while fetching table with id {TableNumber}.", tableNumber);
            return ServiceResponse<TableDto?>.Failure(
                HttpStatusCode.InternalServerError,
                $"An error occured while fetching table with id {tableNumber}.");
        }
    }

    public Task<int> CreateTableAsync(Table newTable)
    {
        throw new NotImplementedException();
    }
}