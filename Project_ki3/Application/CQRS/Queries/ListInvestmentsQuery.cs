using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries;

public record ListInvestmentsQuery(string UserId) : IRequest<IEnumerable<Investment>>;
