using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Queries.UserDetail;

public class UserDetailMapper : Profile
{
    public UserDetailMapper()
    {
        CreateMap<User, UserDetailResponse>();
    }
}
