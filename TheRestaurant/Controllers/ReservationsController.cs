using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/reservations")]
[ApiController]
public class ReservationsController(IReservationService service)
{
    [HttpPost]
    public async Task<IActionResult> CreateReservation(ReservationDto reservationDto) =>
        Generate.ActionResult(await service.CreateReservationAsync(reservationDto));
}