using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement.Application.DTO.Response.Auth;
using EmployeeManagement.Application.Services.Interfaces.JWTtoken;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Shared.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Application.Services.Implementations.JWTtoken;

public class JWTtokenService : IJWTtokenService
{
    private readonly JwtOptions _opt;

    public JWTtokenService(IOptions<JwtOptions> opt)
    {
        _opt = opt.Value;
    }
    public Task<TokenCreationResponse> GenerateToken(User user, Employee employee)
    {
      
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_opt.Key)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),

            new Claim("RoleId", user.RoleId.ToString()),
            new Claim("EmployeeId", employee.Id.ToString()),

            new Claim("FullName", $"{employee.FirstName} {employee.LastName}")
        };

        var expiryMinutes = double.Parse(_opt.AccessTokenMinutes.ToString());
        var expiration = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(new TokenCreationResponse
        {
            AccessToken = tokenString,
            ExpiresAtutc = expiration
        });
    }
}