using Application.CQRS.Queries.Budgets;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository) : IRequestHandler<GetBudgetByIdQuery, Budget?>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<Budget?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _budgetRepository.GetByIdAsync(request.Id);
    }
}