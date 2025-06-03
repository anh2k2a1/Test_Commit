using MediatR;

namespace Application.CQRS.Commands.Users;

public record ForgotPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; } = default!;
    public string ResetLink { get; set; } = default!;
}
