using Application.CQRS.Commands.Transactions;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Transactions;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.DeleteAsync(request.Id);
    }
}