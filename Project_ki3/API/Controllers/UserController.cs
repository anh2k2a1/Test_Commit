using Application.CQRS.DTOs;
using Application.CQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.CQRS.Commands.Users;
using Application.CQRS.DTOs;
using System.Security.Cryptography;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/users
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    // GET: api/users/{id}
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = userId }, null);
    }

    // PUT: api/users/{id}
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand
        {
            Id = id,
            UserName = request.UserName,
            Currency = request.Currency,
            Language = request.Language,
            NotificationEnabled = request.NotificationEnabled
        };

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    // DELETE: api/users/{id}
    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _mediator.Send(new DeleteUserCommand(id));
        return success ? NoContent() : NotFound();
    }


    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        try
        {
            var success = await _mediator.Send(command);
            return success ? NoContent() : BadRequest(new { Message = "Failed to change password." });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { ex.Message });
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest("Email is required.");

        // Generate a secure random token
        var tokenBytes = RandomNumberGenerator.GetBytes(32); // 256 bits
        var token = Convert.ToBase64String(tokenBytes);
        token = Uri.EscapeDataString(token); // Make it URL-safe

        // Build the base URL dynamically from the current request
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var resetLink = $"{baseUrl}/reset-password?email={Uri.EscapeDataString(request.Email)}&token={token}";

        var command = new ForgotPasswordCommand
        {
            Email = request.Email,
            ResetLink = resetLink
        };

        await _mediator.Send(command, cancellationToken);

        return Ok("Password reset email sent if the email exists in our system.");
    }


}
