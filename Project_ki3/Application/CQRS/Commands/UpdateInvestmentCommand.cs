using MediatR;

namespace Application.CQRS.Commands;

public record UpdateInvestmentCommand(
    string Id,
    string Name,
    decimal Amount,
    string Type
) : IRequest<bool>;