using FluentValidation;

namespace App.Application.Features.Commands.UserLogin;

public class UserLoginValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(i => i.EmailAddress).NotEmpty().NotNull().MinimumLength(4).WithMessage("Error Message For Email Address!");
    }
}
