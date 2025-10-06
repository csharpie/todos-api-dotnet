using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Todos.Models;
using Todos.Services;

namespace Todos.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly TokenService _tokenService;

    public LoginController(ILogger<LoginController> logger, TokenService tokenService)
    {
        _logger = logger;
        _tokenService = tokenService;
    }

    [HttpPost(Name = "Login")]
    public IActionResult Login([FromBody] Login model)
    {
        if (model.UserName != Environment.GetEnvironmentVariable("AuthorizedUser") || model.Password != Environment.GetEnvironmentVariable("AuthorizedUserPassword"))
            return Unauthorized();
        
        var token = _tokenService.GenerateToken();
        return Ok(new { token });
    }
}