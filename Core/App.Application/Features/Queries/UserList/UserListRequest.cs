using MediatR;

namespace App.Application.Features.Queries.UserList;

public class UserListRequest : IRequest<UserListResponse>
{
    public int Page { get; set; }
    public int Count { get; set; }
}
