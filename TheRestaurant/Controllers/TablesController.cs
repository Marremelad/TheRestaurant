using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;
[Route("api/tables")]
[ApiController]
public class TablesController(ITableService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTables() =>
        Generate.ActionResult(await service.GetTablesAsync());

    [HttpGet("{table-number:int}")]
    public async Task<IActionResult> GetTable([FromRoute(Name = "table-number")]int tableNumber) => 
        Generate.ActionResult(await service.GetTableAsync(tableNumber));

    [HttpPost]
    public async Task<IActionResult> CreateTable(TableDto tableDto) =>
        Generate.ActionResult(await service.CreateTableAsync(tableDto));

    [HttpDelete("{table-number:int}")]
    public async Task<IActionResult> DeleteTable([FromRoute(Name = "table-number")]int tableNumber) =>
        Generate.ActionResult(await service.DeleteTableAsync(tableNumber));
}