using ApiTemplate.Commands;
using ApiTemplate.Contracts.Requests;
using ApiTemplate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id:guid")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetUserQuery(id);
        var result = await _mediator.Send(query);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetUsersQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var query = new CreateUserCommand(request.Name);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPut("id:guid")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var query = new UpdateUserCommand(id, request);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(
            success => success is null ? NotFound() : Ok(success),
            BadRequest);
    }
}