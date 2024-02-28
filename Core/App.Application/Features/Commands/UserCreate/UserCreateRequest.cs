using MediatR;

namespace App.Application.Features.Commands.UserCreate;

public class UserCreateRequest : IRequest<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
}
