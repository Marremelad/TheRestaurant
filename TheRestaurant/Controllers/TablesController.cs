using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;

namespace TheRestaurant.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TablesController(ITableService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableDto>>> GetTables()
    {
        return Ok(await service.GetTablesAsync());
    }
}