using MediatR;

namespace App.Application.Features.Queries.BlogDetail;

public class BlogDetailRequest : IRequest<BlogDetailResponse>
{
    public Guid BlogId { get; set; }
}
