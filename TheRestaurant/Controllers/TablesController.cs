using Microsoft.AspNetCore.Mvc;
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
}