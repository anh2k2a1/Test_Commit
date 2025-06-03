
using Application.CQRS.Commands;
using Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DebtsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/debts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetDebtByIdQuery(id));
        return result is null ? NotFound("Debt not found.") : Ok(result);
    }

    // GET: api/debts/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> ListByUser(string userId)
    {
        var result = await _mediator.Send(new ListDebtsQuery(userId));
        return Ok(result);
    }

    // POST: api/debts
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDebtCommand command)
    {
        var debtId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = debtId }, null);
    }

    // PUT: api/debts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDebtCommand command)
    {
        if (id != command.Id) return BadRequest("Debt ID mismatch.");
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound("Debt not found.");
    }

    // DELETE: api/debts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteDebtCommand(id));
        return success ? NoContent() : NotFound("Debt not found.");
    }
}