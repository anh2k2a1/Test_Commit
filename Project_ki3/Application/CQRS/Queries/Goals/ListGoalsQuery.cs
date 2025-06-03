using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Goals;

public record ListGoalsQuery(string UserId) : IRequest<IEnumerable<Goal>>;