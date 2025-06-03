using Application.CQRS.Commands.Goals;
using Application.CQRS.Queries.Goals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetGoalByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> ListByUser(string userId)
    {
        var result = await _mediator.Send(new ListGoalsQuery(userId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalCommand command)
    {
        var goalId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = goalId }, null);
    }

    [HttpPost("{id}/set-reminder")]
    public async Task<IActionResult> SetReminder(string id, [FromBody] DateTime reminderAt)
    {
        if (reminderAt <= DateTime.UtcNow)
            return BadRequest("Reminder date must be in the future.");

        var goal = await _mediator.Send(new GetGoalByIdQuery(id));
        if (goal == null) return NotFound("Goal not found.");

        var success = await _mediator.Send(new UpdateGoalCommand(
            goal.Id,
            goal.Title,
            goal.Type,
            goal.Progress,
            goal.Target,
            goal.MilestoneLabels,
            goal.Deadline,
            reminderAt,
            goal.IsCompleted
        ));

        return success ? NoContent() : BadRequest("Failed to set reminder.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateGoalCommand command)
    {
        if (id != command.Id) return BadRequest("Goal ID mismatch.");
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteGoalCommand(id));
        return success ? NoContent() : NotFound();
    }
}
