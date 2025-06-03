using MediatR;

namespace Application.CQRS.Commands.Budgets;

public record DeleteBudgetCommand(string Id) : IRequest<bool>;