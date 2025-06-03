using Application.CQRS.Commands.Budgets;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, string>
{
    private readonly IBudgetRepository _budgetRepository;

    public CreateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<string> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = new Budget
        {
            UserId = request.UserId,
            Type = request.Type,
            Category = request.Category,
            Amount = request.Amount,
            Period = request.Period,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        await _budgetRepository.CreateAsync(budget);
        return budget.Id!;
    }
}