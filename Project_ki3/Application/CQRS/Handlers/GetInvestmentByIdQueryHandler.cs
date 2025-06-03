using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers;

public class GetInvestmentByIdQueryHandler : IRequestHandler<GetInvestmentByIdQuery, Investment?>
{
    private readonly IInvestmentRepository _investmentRepository;

    public GetInvestmentByIdQueryHandler(IInvestmentRepository investmentRepository)
    {
        _investmentRepository = investmentRepository;
    }

    public async Task<Investment?> Handle(GetInvestmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _investmentRepository.GetByIdAsync(request.Id);
    }
}