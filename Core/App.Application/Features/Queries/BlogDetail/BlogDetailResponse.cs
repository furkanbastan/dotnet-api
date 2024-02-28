namespace App.Application.Features.Queries.BlogDetail;

public class BlogDetailResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid UserId { get; set; }
}
