using MediatR;

namespace App.Application.Features.Queries.UserDetail;

public class UserDetailRequest : IRequest<UserDetailResponse>
{
    public Guid UserId { get; set; }
}
