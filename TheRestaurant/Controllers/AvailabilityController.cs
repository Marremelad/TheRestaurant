using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/availability")]
[ApiController]
public class AvailabilityController(IAvailabilityService service)
{
    [HttpPost]
    public async Task<IActionResult> GetAvailableTablesByCustomerInput(AvailabilityRequestDto availabilityRequestDto) =>
        Generate.ActionResult(await service.GetAvailableTablesByCustomerInputAsync(availabilityRequestDto));
}