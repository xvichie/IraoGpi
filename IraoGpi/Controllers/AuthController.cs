using IraoGpi.API.Helpers.Filters;
using IraoGpi.Application.Management.Auth.Requests;
using IraoGpi.Application.Management.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IraoGpi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    [ValidateModel]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.Login(request);

        return Ok(response);
    }
}
