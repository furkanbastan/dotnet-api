using App.Application.Abstractions.Repositories;
using App.Application.Abstractions.Services;
using App.Application.Exceptions;
using App.Application.Features.Common;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Commands.UserLogin;

public class UserLoginHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;
    private readonly ITokenService tokenService;

    public UserLoginHandler(ITokenService tokenService, IMapper mapper, IRepository<User> userRepository)
    {
        this.tokenService = tokenService;
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetAsync(i => i.EmailAddress == request.EmailAddress)
            ?? throw new DatabaseValidationException("User Not Found!");

        var pass = PasswordEncryptor.Encrypt(request.Password!);

        if (pass != userDb.Password)
            throw new DatabaseValidationException("Password Or Email Is Wrong!");

        var response = mapper.Map<UserLoginResponse>(userDb);
        response.Token = tokenService.CreateAccessToken(userDb);

        return response;
    }
}
