using Application.CQRS.Commands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers;

public class CreateInvestmentCommandHandler : IRequestHandler<CreateInvestmentCommand, string>
{
    private readonly IInvestmentRepository _investmentRepository;

    public CreateInvestmentCommandHandler(IInvestmentRepository investmentRepository)
    {
        _investmentRepository = investmentRepository;
    }

    public async Task<string> Handle(CreateInvestmentCommand request, CancellationToken cancellationToken)
    {
        var investment = new Investment
        {
            UserId = request.UserId,
            Name = request.Name,
            AmountInvested = request.Amount,
            Type = request.Type,
            CreatedAt = DateTime.UtcNow
        };

        await _investmentRepository.CreateAsync(investment);
        return investment.Id!;
    }
}