using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries;

public record GetInvestmentByIdQuery(string Id) : IRequest<Investment?>;