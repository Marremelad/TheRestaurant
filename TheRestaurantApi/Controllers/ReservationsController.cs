using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetReservations() =>
        Generate.ActionResult(await service.GetReservationsAsync());

    [HttpGet("{reservation-email}")]
    [Authorize]
    public async Task<IActionResult> GetReservationsByEmail([FromRoute(Name = "reservation-email")] string reservationEmail) =>
        Generate.ActionResult(await service.GetReservationsByEmailAsync(reservationEmail));
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation(ReservationCreateDto reservationCreateDto) =>
        Generate.ActionResult(await service.CreateReservationAsync(reservationCreateDto.PersonalInfo, reservationCreateDto.ReservationHoldId));

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteReservation([FromRoute(Name = "id")] int id) => 
        Generate.ActionResult(await service.DeleteReservationAsync(id));
}