using Application.CQRS.Commands.Transactions;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Transactions;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);
        if (transaction == null) return false;

        transaction.Amount = request.Amount;
        transaction.Category = request.Category;
        transaction.Note = request.Note;
        transaction.ReminderAt = request.ReminderAt;

        return await _transactionRepository.UpdateAsync(transaction);
    }
}