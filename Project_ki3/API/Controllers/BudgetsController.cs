using Application.CQRS.Commands.Budgets;
using Application.CQRS.Queries.Budgets;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/budgets/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Budget), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetBudgetByIdQuery(id));
        return result is null ? NotFound("Budget not found.") : Ok(result);
    }

    // GET: api/budgets/user/{userId}
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<Budget>), 200)]
    public async Task<IActionResult> ListByUser(string userId)
    {
        var result = await _mediator.Send(new ListBudgetsQuery(userId));
        return Ok(result);
    }

    // POST: api/budgets
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCommand command)
    {
        var budgetId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = budgetId }, null);
    }

    // PUT: api/budgets/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateBudgetCommand command)
    {
        if (id != command.Id) return BadRequest("Budget ID mismatch.");
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound("Budget not found.");
    }

    // DELETE: api/budgets/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteBudgetCommand(id));
        return success ? NoContent() : NotFound("Budget not found.");
    }

    /// <summary>
    /// Get budget report for the current and previous month, grouped by type.
    /// </summary>
    /// <param name="userId">User ID (MongoDB ObjectId as string)</param>
    /// <param name="cancellationToken"></param>
    /// <returns>ReportBudgetResult</returns>
    [HttpGet("report")]
    [ProducesResponseType(typeof(ReportBudgetResult), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReport([FromQuery] string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return BadRequest("UserId is required.");

        var query = new GetReportBudgetQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
