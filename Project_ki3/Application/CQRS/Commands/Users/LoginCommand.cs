using MediatR;

namespace Application.CQRS.Commands.Users;

public record LoginCommand(string Email, string Password) : IRequest<string>;