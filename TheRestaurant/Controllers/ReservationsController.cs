using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationsController(IReservationService service)
{
    [HttpGet]
    public async Task<IActionResult> GetReservations() =>
        Generate.ActionResult(await service.GetReservationsAsync());

    [HttpGet("{reservation-email}")]
    public async Task<IActionResult> GetReservationsByEmail([FromRoute(Name = "reservation-email")] string reservationEmail) =>
        Generate.ActionResult(await service.GetReservationsByEmailAsync(reservationEmail));
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation(ReservationDto reservationDto) =>
        Generate.ActionResult(await service.CreateReservationAsync(reservationDto));

    [HttpDelete("{reservation-email}")]
    public async Task<IActionResult> DeleteReservation([FromRoute(Name = "reservation-email")] string reservationEmail) => 
        Generate.ActionResult(await service.DeleteReservationAsync(reservationEmail));
}