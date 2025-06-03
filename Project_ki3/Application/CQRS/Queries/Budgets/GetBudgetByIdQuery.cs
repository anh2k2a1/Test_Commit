using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Budgets;

public record GetBudgetByIdQuery(string Id) : IRequest<Budget?>;