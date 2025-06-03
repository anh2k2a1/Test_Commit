using MediatR;

namespace Application.CQRS.Commands.Transactions;

public record CreateTransactionCommand(
    string UserId,
    decimal Amount,
    string Category,
    string? Note,
    DateTime? ReminderAt
) : IRequest<string>;
