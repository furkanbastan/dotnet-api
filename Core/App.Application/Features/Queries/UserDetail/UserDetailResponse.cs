namespace App.Application.Features.Queries.UserDetail;

public class UserDetailResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
}
