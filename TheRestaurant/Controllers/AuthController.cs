// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using TheRestaurant.DTOs;
using TheRestaurant.Services.IServices;
using TheRestaurant.Utilities;

namespace TheRestaurant.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto) =>
        Generate.ActionResult(await authService.LoginAsync(loginDto));

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken) =>
        Generate.ActionResult(await authService.RefreshTokenAsync(refreshToken));
}