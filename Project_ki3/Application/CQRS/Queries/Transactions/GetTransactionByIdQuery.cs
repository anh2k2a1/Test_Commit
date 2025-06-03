using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Transactions;

public record GetTransactionByIdQuery(string Id) : IRequest<Transaction?>;