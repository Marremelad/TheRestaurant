using TheRestaurant.DTOs;
using TheRestaurant.Models;

namespace TheRestaurant.Mappers;

public class TableMapper
{
    public static TableDto ToDto(Table table) => new(table.Number, table.Capacity);
    public static List<TableDto> ToDtos(IEnumerable<Table> tables) => tables.Select(ToDto).ToList();
    
    public static Table ToEntity(TableDto tableDto) => new() { Number = tableDto.Number, Capacity = tableDto.Capacity };
    public static List<Table> ToEntities(IEnumerable<TableDto> tableDtos) => tableDtos.Select(ToEntity).ToList();
}