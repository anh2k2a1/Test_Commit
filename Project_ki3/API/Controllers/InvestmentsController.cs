using Application.CQRS.Commands;
 
using Application.CQRS.Queries;
 
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InvestmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvestmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/investments/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetInvestmentByIdQuery(id));
        return result is null ? NotFound("Investment not found.") : Ok(result);
    }

    // GET: api/investments/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> ListByUser(string userId)
    {
        var result = await _mediator.Send(new ListInvestmentsQuery(userId));
        return Ok(result);
    }

    // POST: api/investments
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvestmentCommand command)
    {
        var investmentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = investmentId }, null);
    }

    // PUT: api/investments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateInvestmentCommand command)
    {
        if (id != command.Id) return BadRequest("Investment ID mismatch.");
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound("Investment not found.");
    }

    // DELETE: api/investments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteInvestmentCommand(id));
        return success ? NoContent() : NotFound("Investment not found.");
    }
}