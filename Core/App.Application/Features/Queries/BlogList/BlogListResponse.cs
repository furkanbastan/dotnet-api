using App.Application.Features.Queries.BlogDetail;
using App.Domain.Models;

namespace App.Application.Features.Queries.BlogList;

public class BlogListResponse
{
    public PageModel? PageInfo { get; set; }
    public List<BlogDetailResponse>? Blogs { get; set; }
}
