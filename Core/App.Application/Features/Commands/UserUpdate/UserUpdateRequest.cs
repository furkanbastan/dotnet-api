using MediatR;

namespace App.Application.Features.Commands.UserUpdate;

public class UserUpdateRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
}
