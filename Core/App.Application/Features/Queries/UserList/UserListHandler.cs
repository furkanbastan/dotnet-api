using App.Application.Abstractions.Repositories;
using App.Application.Features.Queries.UserDetail;
using App.Domain.Entities;
using App.Domain.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Queries.UserList;

public class UserListHandler : IRequestHandler<UserListRequest, UserListResponse>
{
    private readonly IRepository<User> userRepository;
    private readonly IMapper mapper;

    public UserListHandler(IMapper mapper, IRepository<User> userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<UserListResponse> Handle(UserListRequest request, CancellationToken cancellationToken)
    {
        var query = userRepository.AsQueryable();
        var rowsCount = await query.CountAsync(cancellationToken: cancellationToken);
        var response = new UserListResponse();

        if (request.Page != 0 && request.Count != 0)
        {
            var pageInfo = new PageModel(
                CurrentPage: request.Page,
                PageCapability: request.Count,
                TotalRowCount: rowsCount
            );

            query = query.Skip(pageInfo.Skipped).Take(pageInfo.PageCapability);
            response.PageInfo = pageInfo;
        }

        var userList = await query.ToListAsync(cancellationToken: cancellationToken);
        response.Users = mapper.Map<List<UserDetailResponse>>(userList);

        return response;
    }
}
