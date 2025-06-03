using Application.CQRS.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers;

public class UpdateDebtCommandHandler : IRequestHandler<UpdateDebtCommand, bool>
{
    private readonly IDebtRepository _debtRepository;

    public UpdateDebtCommandHandler(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<bool> Handle(UpdateDebtCommand request, CancellationToken cancellationToken)
    {
        var debt = await _debtRepository.GetByIdAsync(request.Id);
        if (debt == null) return false;

        debt.Creditor = request.Creditor;
        debt.Type = request.Type;
        debt.TotalAmount = request.TotalAmount;
        debt.RemainingAmount = request.RemainingAmount;
        debt.MonthlyPayment = request.MonthlyPayment;
        debt.NextDueDate = request.NextDueDate;

        return await _debtRepository.UpdateAsync(debt);
    }
}