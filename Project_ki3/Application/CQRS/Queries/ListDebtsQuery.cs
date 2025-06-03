using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries;

public record ListDebtsQuery(string UserId) : IRequest<IEnumerable<Debt>>;
