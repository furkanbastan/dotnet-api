using MediatR;

namespace App.Application.Features.Commands.UserLogin;

public class UserLoginRequest : IRequest<UserLoginResponse>
{
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
}
