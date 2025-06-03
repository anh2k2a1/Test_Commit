using MediatR;

namespace Application.CQRS.Commands.Budgets;

public record UpdateBudgetCommand(
    string Id,
    string Type,
    string Category,
    decimal Amount,
    string Period,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<bool>;