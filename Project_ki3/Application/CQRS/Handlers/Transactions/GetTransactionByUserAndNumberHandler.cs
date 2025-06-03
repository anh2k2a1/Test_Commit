using Application.CQRS.Queries.Transactions;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Transactions;

public class GetTransactionByUserAndNumberHandler : IRequestHandler<GetTransactionByUserAndNumberQuery, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByUserAndNumberHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(GetTransactionByUserAndNumberQuery request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetByUserIdAndNumberAsync(request.UserId, request.Number, cancellationToken);
    }
}
