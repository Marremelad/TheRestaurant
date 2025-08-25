using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TablesController(ITableService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTables() =>
        Generate.ActionResult(await service.GetTablesAsync());

    [HttpGet("{tableNumber:int}")]
    public async Task<IActionResult> GetTable(int tableNumber) => 
        Generate.ActionResult(await service.GetTableAsync(tableNumber));

    [HttpPost]
    public async Task<IActionResult> CreateTable(TableDto tableDto) =>
        Generate.ActionResult(await service.CreateTableAsync(tableDto));
}