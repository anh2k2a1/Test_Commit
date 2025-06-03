using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries;

public record GetDebtByIdQuery(string Id) : IRequest<Debt?>;
