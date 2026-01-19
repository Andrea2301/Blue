using Blue.Application.Common.Interfaces;
using Blue.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blue.Infrastructure.Security;

public class TokenGenerator : ITokenGenerator
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenGenerator(IConfiguration configuration)
    {
        _key = configuration["Jwt:Key"]
               ?? throw new InvalidOperationException("JWT Key not configured");

        _issuer = configuration["Jwt:Issuer"]
                  ?? throw new InvalidOperationException("JWT Issuer not configured");

        _audience = configuration["Jwt:Audience"]
                    ?? throw new InvalidOperationException("JWT Audience not configured");
    }

    public string Generate(User user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_key);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}