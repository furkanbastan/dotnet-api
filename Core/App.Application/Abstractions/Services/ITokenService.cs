using App.Domain.Entities;

namespace App.Application.Abstractions.Services;

public interface ITokenService
{
    string CreateAccessToken(User user);
}
