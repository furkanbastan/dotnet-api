using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Commands.UserCreate;

public class UserCreateHandler : IRequestHandler<UserCreateRequest, Guid>
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;

    public UserCreateHandler(IRepository<User> userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetAsync(i => i.EmailAddress == request.EmailAddress);

        if (userDb is not null)
            throw new DatabaseValidationException("User Already Exist!");

        var user = mapper.Map<User>(request);

        await userRepository.AddAsync(user);
        await userRepository.SaveAsync();

        return user.Id;

        //Email Changed/Created => RabbitMq'ya kayıt atılabilir.
    }
}
