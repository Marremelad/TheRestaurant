using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetTables() =>
        Generate.ActionResult(await service.GetTablesAsync());

    [HttpGet("{table-number:int}")]
    [Authorize]
    public async Task<IActionResult> GetTableByNumber([FromRoute(Name = "table-number")]int tableNumber) => 
        Generate.ActionResult(await service.GetTableByTableNumberAsync(tableNumber));

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTable(TableDto tableDto) =>
        Generate.ActionResult(await service.CreateTableAsync(tableDto));

    [HttpPatch("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateTable([FromRoute(Name = "id")] int id, TableUpdateDto dto) =>
        Generate.ActionResult(await service.UpdateTableAsync(id, dto));

    [HttpDelete("{table-number:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteTable([FromRoute(Name = "table-number")]int tableNumber) =>
        Generate.ActionResult(await service.DeleteTableAsync(tableNumber));
}