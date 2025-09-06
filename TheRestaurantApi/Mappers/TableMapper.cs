using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public static class TableMapper
{
    public static TableDto ToDto(Table table) => new()
    {
        Number = table.Number,
        Capacity = table.Capacity
    };
    
    public static List<TableDto> ToDtos(IEnumerable<Table> tables) => tables.Select(ToDto).ToList();
    
    public static Table ToEntity(TableDto tableDto) => new() { Number = tableDto.Number, Capacity = tableDto.Capacity };
}