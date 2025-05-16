using DownTown.Dto;
using DownTown.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DownTown.Controller;

[Route("api")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto loginDto, IConfiguration configuration)
    {
        var result = await _authenticationService.Login(loginDto, configuration);
        return StatusCode(result.StatusCode, result);
    }
}