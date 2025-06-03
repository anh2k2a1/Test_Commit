using Application.CQRS.Commands.Transactions;
using Application.CQRS.Queries.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetTransactionByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> ListByUser(string userId)
    {
        var result = await _mediator.Send(new ListTransactionsQuery(userId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        var transactionId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = transactionId }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateTransactionCommand command)
    {
        if (id != command.Id) return BadRequest("Transaction ID mismatch.");
        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteTransactionCommand(id));
        return success ? NoContent() : NotFound();
    }

    [HttpGet("user/{userId}/recent")]
    public async Task<IActionResult> GetRecentTransactions(string userId, [FromQuery] int number)
    {
        if (number <= 0)
            return BadRequest("Number must be greater than 0.");

        var result = await _mediator.Send(new GetTransactionByUserAndNumberQuery(userId, number));
        return Ok(result);
    }

}