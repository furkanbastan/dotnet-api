using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Commands.UserUpdate;

public class UserUpdateMapper : Profile
{
    public UserUpdateMapper()
    {
        CreateMap<User, UserUpdateRequest>().ReverseMap();
    }
}
