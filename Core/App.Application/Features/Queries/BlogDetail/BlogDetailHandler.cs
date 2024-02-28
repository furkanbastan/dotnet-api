using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Domain.Entities;
using AutoMapper;
using MediatR;

namespace App.Application.Features.Queries.BlogDetail;

public class BlogDetailHandler : IRequestHandler<BlogDetailRequest, BlogDetailResponse>
{
    private readonly IRepository<Blog> blogRepository;
    private readonly IMapper mapper;

    public BlogDetailHandler(IMapper mapper, IRepository<Blog> blogRepository)
    {
        this.mapper = mapper;
        this.blogRepository = blogRepository;
    }

    public async Task<BlogDetailResponse> Handle(BlogDetailRequest request, CancellationToken cancellationToken)
    {
        var blogDb = await blogRepository.GetAsync(i => i.Id == request.BlogId)
            ?? throw new DatabaseValidationException("Blog Is Not Found!");

        var blogMap = mapper.Map<BlogDetailResponse>(blogDb);

        return blogMap;
    }
}
