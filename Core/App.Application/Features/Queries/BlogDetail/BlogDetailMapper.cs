using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Queries.BlogDetail;

public class BlogDetailMapper : Profile
{
    public BlogDetailMapper()
    {
        CreateMap<Blog, BlogDetailResponse>();
    }
}
