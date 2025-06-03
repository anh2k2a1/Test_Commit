using MediatR;

namespace Application.CQRS.Commands;

public record UpdateDebtCommand(
    string Id,
    string Creditor,
    string Type,
    decimal TotalAmount,
    decimal RemainingAmount,
    decimal MonthlyPayment,
    DateTime NextDueDate
) : IRequest<bool>;
