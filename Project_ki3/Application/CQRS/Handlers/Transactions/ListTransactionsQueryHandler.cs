using Application.CQRS.Queries.Transactions;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Transactions;

public class ListTransactionsQueryHandler : IRequestHandler<ListTransactionsQuery, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public ListTransactionsQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(ListTransactionsQuery request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetByUserIdAsync(request.UserId);
    }
}