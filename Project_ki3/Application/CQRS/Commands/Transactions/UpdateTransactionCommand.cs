using MediatR;

namespace Application.CQRS.Commands.Transactions;

public record UpdateTransactionCommand(
    string Id,
    decimal Amount,
    string Category,
    string? Note,
    DateTime? ReminderAt
) : IRequest<bool>;