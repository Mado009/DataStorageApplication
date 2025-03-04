using DataStorageAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }


    private string GenerateJwtToken(string username)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin") 
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


