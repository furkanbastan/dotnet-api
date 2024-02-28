using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Application.Abstractions.Services;
using App.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration configuration;
    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string CreateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]!.ToString()));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiry = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(
            expires: expiry,
            signingCredentials: cred,
            notBefore: DateTime.Now,
            claims: [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.EmailAddress!),
                new(ClaimTypes.GivenName, user.FirstName!),
                new(ClaimTypes.Surname, user.LastName!),
                //new(ClaimTypes.Name, user.Username!),
            ]
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
// kaynak = https://github.com/gncyyldz/Mini-E-Ticaret-API/blob/master/ETicaretAPI/Infrastructure/ETicaretAPI.Infrastructure/Services/Token/TokenHandler.cs