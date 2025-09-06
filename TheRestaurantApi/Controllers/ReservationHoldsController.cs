using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/reservation-holds")]
[ApiController]
public class ReservationHoldsController(IReservationHoldService service): ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetReservationHolds() =>
        Generate.ActionResult(await service.GetReservationHoldsAsync());
    
    [HttpPost]
    public async Task<IActionResult> CreateReservationHold(AvailabilityProcessorDto availabilityProcessorDto) =>
        Generate.ActionResult(await service.CreateReservationHoldAsync(availabilityProcessorDto));
}