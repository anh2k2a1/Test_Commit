using Application.CQRS.Queries.Budgets;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class ListBudgetsQueryHandler(IBudgetRepository budgetRepository) : IRequestHandler<ListBudgetsQuery, IEnumerable<Budget>>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<IEnumerable<Budget>> Handle(ListBudgetsQuery request, CancellationToken cancellationToken)
    {
        return await _budgetRepository.GetByUserIdAsync(request.UserId);
    }
}