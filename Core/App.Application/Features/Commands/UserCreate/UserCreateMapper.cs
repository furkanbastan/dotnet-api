using App.Application.Features.Common;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Commands.UserCreate;

public class UserCreateMapper : Profile
{
    public UserCreateMapper()
    {
        CreateMap<UserCreateRequest, User>()
            .ForMember(target => target.Password, opt => opt.MapFrom(src => PasswordEncryptor.Encrypt(src.Password!)));
    }
}
