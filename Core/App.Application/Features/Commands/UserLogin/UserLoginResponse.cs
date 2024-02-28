namespace App.Application.Features.Commands.UserLogin;

public class UserLoginResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? Token { get; set; }
}
