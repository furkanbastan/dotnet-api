using App.Application.Features.Commands.UserCreate;
using App.Application.Features.Commands.UserLogin;
using App.Application.Features.Queries.BlogList;
using App.Application.Features.Queries.UserDetail;
using App.Application.Features.Queries.UserList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator mediator;

    public UsersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] UserListRequest request)
    {
        var result = mediator.Send(request);
        return Ok(result.Result);
    }

    [HttpGet("{UserId}")]
    public IActionResult Get([FromRoute] UserDetailRequest request)
    {
        var response = mediator.Send(request);
        return Ok(response.Result);
    }

    [HttpGet("{UserId}/blogs")]
    public IActionResult Get([FromRoute] Guid UserId, [FromQuery] BlogListRequest request)
    {
        request.UserId = UserId;
        var response = mediator.Send(request);
        return Ok(response.Result);
    }

    [HttpPost]
    public IActionResult Create([FromBody] UserCreateRequest request)
    {
        var response = mediator.Send(request);
        return Ok(response.Result);
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login([FromBody] UserLoginRequest request)
    {
        var response = mediator.Send(request);
        return Ok(response.Result);
    }
}
