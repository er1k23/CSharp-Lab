using JobRecruitmentApi.Authentication;
using JobRecruitmentApi.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitmentApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var user = await _authService.RegisterAsync(
            request.UserName,
            request.Password);

        if (user is null)
        {
            return Conflict(new
            {
                message = "Username is already taken."
            });
        }

        return Ok(new
        {
            userID = user.Id,
            userName = user.UserName
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        var user = await _authService.LoginAsync(request.UserName, request.Password);

        if (user is null)
        {
            return BadRequest(new
            {
                message = "Invalid username or passowrd."
            });
        }

        return Ok(new
        {
            userId = user.Id,
            userName = user.UserName
        });
    }
}