using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Goals;

public record GetGoalByIdQuery(string Id) : IRequest<Goal?>;