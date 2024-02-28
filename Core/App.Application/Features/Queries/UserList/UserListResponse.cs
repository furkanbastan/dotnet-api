using App.Application.Features.Queries.UserDetail;
using App.Domain.Models;

namespace App.Application.Features.Queries.UserList;

public class UserListResponse
{
    public PageModel? PageInfo { get; set; }
    public List<UserDetailResponse>? Users { get; set; }
}
