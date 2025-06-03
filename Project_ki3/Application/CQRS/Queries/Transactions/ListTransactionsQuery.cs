using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.Transactions;

public record ListTransactionsQuery(string UserId) : IRequest<IEnumerable<Transaction>>;
