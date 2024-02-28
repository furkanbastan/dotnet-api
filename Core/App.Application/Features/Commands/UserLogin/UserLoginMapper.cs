using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Commands.UserLogin;

public class UserLoginMapper : Profile
{
    public UserLoginMapper()
    {
        CreateMap<User, UserLoginResponse>().ReverseMap();
    }
}
