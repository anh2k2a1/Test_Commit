using Application.CQRS.Queries.Budgets;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Budgets;

public class GetReportBudgetHandler(IBudgetRepository budgetRepository) : IRequestHandler<GetReportBudgetQuery, ReportBudgetResult>
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<ReportBudgetResult> Handle(GetReportBudgetQuery request, CancellationToken cancellationToken)
    {
        return await _budgetRepository.GetReportByUserIdAsync(request.UserId, cancellationToken);
    }
}
