using Application.CQRS.Commands.Transactions;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Transactions;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<string> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction
        {
            UserId = request.UserId,
            Amount = request.Amount,
            Category = request.Category,
            Note = request.Note,
            ReminderAt = request.ReminderAt
        };

        await _transactionRepository.CreateAsync(transaction);
        return transaction.Id!;
    }
}