using Blue.Application.Auth.Commands.RegisterUser;
using Blue.Application.Auth.Commands;
using Blue.Application.Auth.DTOs;
using Blue.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserCommand _registerUser;
    private readonly LoginUserCommand _loginUser;
    private readonly RefreshTokenCommand _refreshToken;

    public AuthController(
        RegisterUserCommand registerUser,
        LoginUserCommand loginUser,
            RefreshTokenCommand refreshToken )
    {
        _registerUser = registerUser;
        _loginUser = loginUser;
        _refreshToken = refreshToken;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            await _registerUser.ExecuteAsync(request);
            return Ok("User created");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _loginUser.ExecuteAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var response = await _refreshToken.ExecuteAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
} 