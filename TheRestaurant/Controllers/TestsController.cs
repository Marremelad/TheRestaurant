using Microsoft.AspNetCore.Mvc;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/tests")]
[ApiController]
public class TestsController(TableTimeSlotMatrix matrix) : ControllerBase
{
    [HttpGet]
    public IActionResult TestMatrix()
    {
        Console.WriteLine($"\n\n\n\n\n\n\n{matrix.Matrix.Count}Hello, World!\n\n\n\n\n\n\n");
        return Ok(matrix.Matrix);
    }
}