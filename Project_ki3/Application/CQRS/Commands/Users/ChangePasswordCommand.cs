using MediatR;

namespace Application.CQRS.Commands.Users;

public record ChangePasswordCommand(
    string Email,
    string CurrentPassword,
    string NewPassword
) : IRequest<bool>;
