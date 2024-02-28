using MediatR;

namespace App.Application.Features.Commands.UserChangePassword;

public class UserChangePasswordRequest : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}
