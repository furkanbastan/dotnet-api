using App.Application.Features.Queries.BlogDetail;
using App.Application.Features.Queries.BlogList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsController : ControllerBase
{
    private readonly IMediator mediator;

    public BlogsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] BlogListRequest request)
    {
        var response = mediator.Send(request);
        return Ok(response.Result);
    }

    [HttpGet("{BlogId}")]
    public IActionResult Get([FromRoute] BlogDetailRequest request)
    {
        var response = mediator.Send(request);
        return Ok(response.Result);
    }
}
