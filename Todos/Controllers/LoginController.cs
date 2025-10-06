using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Todos.Models;

namespace Todos.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly ILogger<TokenController> _logger;

    public TokenController(ILogger<TokenController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (model.UserName != "brad" || model.DoorCode != Environment.GetEnvironmentVariable("DoorCode"))
            return Unauthorized();
        var token = GenerateJwtToken(model.UserName);
        return Ok(new { token });
    }
    
    private string GenerateJwtToken(string userName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey("super-secret-key"u8.ToArray());
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "FantasyApp",
            audience: "FantasyUsers",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}