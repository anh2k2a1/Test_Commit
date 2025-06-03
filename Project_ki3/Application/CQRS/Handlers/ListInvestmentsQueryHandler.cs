using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers;

public class ListInvestmentsQueryHandler : IRequestHandler<ListInvestmentsQuery, IEnumerable<Investment>>
{
    private readonly IInvestmentRepository _investmentRepository;

    public ListInvestmentsQueryHandler(IInvestmentRepository investmentRepository)
    {
        _investmentRepository = investmentRepository;
    }

    public async Task<IEnumerable<Investment>> Handle(ListInvestmentsQuery request, CancellationToken cancellationToken)
    {
        // Fetch investments for the specified user
        var investments = await _investmentRepository.GetByUserIdAsync(request.UserId);
        return investments;
    }
}