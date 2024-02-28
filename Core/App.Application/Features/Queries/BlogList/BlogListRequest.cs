using MediatR;

namespace App.Application.Features.Queries.BlogList;

public class BlogListRequest : IRequest<BlogListResponse>
{
    public int Page { get; set; }
    public int Count { get; set; }
    public Guid? UserId { get; set; }
}
