using Application.CQRS.Commands.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users;

public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IEmailService _emailService;
    public ForgotPasswordHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        await _emailService.SendPasswordResetEmailAsync(request.Email, request.ResetLink, cancellationToken);
        return Unit.Value;
    }
}
