using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Commands.UserUpdate;

public class UserUpdateHandler : IRequestHandler<UserUpdateRequest, Guid>
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;

    public UserUpdateHandler(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetAsync(i => i.Id == request.Id)
            ?? throw new DatabaseValidationException("User Not Found!");

        userDb = mapper.Map(request, userDb);

        await userRepository.UpdateAsync(userDb);
        await userRepository.SaveAsync();

        return userDb.Id;
    }
}
