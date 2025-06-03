using MediatR;

namespace Application.CQRS.Commands.Transactions;

public record DeleteTransactionCommand(string Id) : IRequest<bool>;