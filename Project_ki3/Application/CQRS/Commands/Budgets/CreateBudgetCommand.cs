using MediatR;

namespace Application.CQRS.Commands.Budgets;

public record CreateBudgetCommand(
    string UserId,
    string Type,
    string Category,
    decimal Amount,
    string Period,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<string>;