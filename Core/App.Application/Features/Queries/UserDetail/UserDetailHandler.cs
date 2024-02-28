using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Queries.UserDetail;

public class UserDetailHandler : IRequestHandler<UserDetailRequest, UserDetailResponse>
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;

    public UserDetailHandler(IMapper mapper, IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<UserDetailResponse> Handle(UserDetailRequest request, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetAsync(i => i.Id == request.UserId)
            ?? throw new DatabaseValidationException("User Not Found!");

        var userMap = mapper.Map<UserDetailResponse>(userDb);

        return userMap;
    }
}
