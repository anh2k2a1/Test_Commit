using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Budgets
{
    public record GetReportBudgetQuery : IRequest<ReportBudgetResult>, IBaseRequest, IEquatable<GetReportBudgetQuery>
    {
        public string UserId { get; init; }

        // Add a constructor to fix CS1729
        public GetReportBudgetQuery(string userId)
        {
            UserId = userId;
        }
    }
}
