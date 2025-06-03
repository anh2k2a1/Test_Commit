using Application.CQRS.Commands.Budgets;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
{
    private readonly IBudgetRepository _budgetRepository;

    public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        return await _budgetRepository.DeleteAsync(request.Id);
    }
}