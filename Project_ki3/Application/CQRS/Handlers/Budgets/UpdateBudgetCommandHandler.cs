using Application.CQRS.Commands.Budgets;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, bool>
{
    private readonly IBudgetRepository _budgetRepository;

    public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<bool> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetByIdAsync(request.Id);
        if (budget == null) return false;

        budget.Category = request.Category;
        budget.Type = request.Type;
        budget.Amount = request.Amount;
        budget.Period = request.Period;
        budget.StartDate = request.StartDate;
        budget.EndDate = request.EndDate;
        budget.UpdatedAt = DateTime.UtcNow;

        return await _budgetRepository.UpdateAsync(budget);
    }
}