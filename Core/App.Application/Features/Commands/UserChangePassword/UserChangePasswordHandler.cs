using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Application.Features.Common;
using App.Domain.Entities;
using MediatR;

namespace App.Application.Features.Commands.UserChangePassword;

public class UserChangePasswordHandler : IRequestHandler<UserChangePasswordRequest, bool>
{
    private readonly IRepository<User> userRepository;

    public UserChangePasswordHandler(IRepository<User> userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<bool> Handle(UserChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var userDb = await userRepository.GetAsync(i => i.Id == request.UserId)
            ?? throw new DatabaseValidationException("User Not Found!");

        var encPass = PasswordEncryptor.Encrypt(request.OldPassword!);

        if (encPass != userDb.Password)
            throw new DatabaseValidationException("Old Password Is Wrong!");

        userDb.Password = PasswordEncryptor.Encrypt(request.NewPassword!);

        await userRepository.UpdateAsync(userDb);
        await userRepository.SaveAsync();

        return true;
    }
}
