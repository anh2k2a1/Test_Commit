using MediatR;

namespace Application.CQRS.Commands;

public record CreateInvestmentCommand(
    string UserId,
    string Name,
    decimal Amount,
    string Type
) : IRequest<string>;
