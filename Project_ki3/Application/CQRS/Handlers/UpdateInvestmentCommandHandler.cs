using Application.CQRS.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers;

public class UpdateInvestmentCommandHandler : IRequestHandler<UpdateInvestmentCommand, bool>
{
    private readonly IInvestmentRepository _investmentRepository;

    public UpdateInvestmentCommandHandler(IInvestmentRepository investmentRepository)
    {
        _investmentRepository = investmentRepository;
    }

    public async Task<bool> Handle(UpdateInvestmentCommand request, CancellationToken cancellationToken)
    {
        var investment = await _investmentRepository.GetByIdAsync(request.Id);
        if (investment == null) return false;

        investment.Name = request.Name;
        investment.AmountInvested = request.Amount;
        investment.Type = request.Type;
        investment.CreatedAt = DateTime.UtcNow;

        return await _investmentRepository.UpdateAsync(investment);
    }
}