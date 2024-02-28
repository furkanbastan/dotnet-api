using App.Application.Abstractions.Repositories;
using App.Application.Exceptions;
using App.Application.Features.Queries.BlogDetail;
using App.Domain.Entities;
using App.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Queries.BlogList;

public class BlogListHandler : IRequestHandler<BlogListRequest, BlogListResponse>
{
    private readonly IRepository<Blog> blogRepository;
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;

    public BlogListHandler(IMapper mapper, IRepository<Blog> blogRepository, IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.blogRepository = blogRepository;
        this.userRepository = userRepository;
    }
    public async Task<BlogListResponse> Handle(BlogListRequest request, CancellationToken cancellationToken)
    {
        var query = blogRepository.AsQueryable();
        var rowsCount = await blogRepository.CountAsync();
        var response = new BlogListResponse();

        if (request.UserId is not null)
        {

            var hasUser = await userRepository.AnyAsync(i => i.Id == request.UserId);
            if (!hasUser) throw new DatabaseValidationException("User Not Found!");

            var hasBlogOfUser = await blogRepository.AnyAsync(i => i.UserId == request.UserId);
            if (!hasBlogOfUser) throw new DatabaseValidationException("User Has Not Blog!");

            query = query.Where(i => i.UserId == request.UserId);
            rowsCount = await query.CountAsync(cancellationToken: cancellationToken);
        }

        if (request.Count != 0 && request.Page != 0)
        {
            var pageInfo = new PageModel(
                CurrentPage: request.Page,
                PageCapability: request.Count,
                TotalRowCount: rowsCount
            );

            query = query.Skip(pageInfo.Skipped).Take(pageInfo.PageCapability);
            response.PageInfo = pageInfo;
        }

        var blogList = await query.ToListAsync(cancellationToken: cancellationToken);
        response.Blogs = mapper.Map<List<BlogDetailResponse>>(blogList);

        return response;
    }
}
