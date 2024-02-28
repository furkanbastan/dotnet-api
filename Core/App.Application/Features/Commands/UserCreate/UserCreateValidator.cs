using FluentValidation;

namespace App.Application.Features.Commands.UserCreate;

public class UserCreateValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateValidator()
    {
        RuleFor(i => i.EmailAddress)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email Address cannot be empty!");
    }
}
