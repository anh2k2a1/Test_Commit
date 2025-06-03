using MediatR;

namespace Application.CQRS.Commands;

public record DeleteDebtCommand(string Id) : IRequest<bool>;