using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Budgets;

public record ListBudgetsQuery(string UserId) : IRequest<IEnumerable<Budget>>;

