using Application.CQRS.Commands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Debts;

public class CreateDebtCommandHandler : IRequestHandler<CreateDebtCommand, string>
{
    private readonly IDebtRepository _debtRepository;

    public CreateDebtCommandHandler(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<string> Handle(CreateDebtCommand request, CancellationToken cancellationToken)
    {
        var debt = new Debt
        {
            UserId = request.UserId,
            Creditor = request.Creditor,
            Type = request.Type,
            TotalAmount = request.TotalAmount,
            RemainingAmount = request.RemainingAmount,
            MonthlyPayment = request.MonthlyPayment,
            NextDueDate = request.NextDueDate,
            CreatedAt = DateTime.UtcNow
        };

        await _debtRepository.CreateAsync(debt);
        return debt.Id!;
    }
}