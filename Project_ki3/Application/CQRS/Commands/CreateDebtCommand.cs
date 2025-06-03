using MediatR;

namespace Application.CQRS.Commands;

public record CreateDebtCommand(
    string UserId,
    string Creditor,
    string Type,
    decimal TotalAmount,
    decimal RemainingAmount,
    decimal MonthlyPayment,
    DateTime NextDueDate
) : IRequest<string>;