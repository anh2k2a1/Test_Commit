using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers;

public class ListDebtsQueryHandler : IRequestHandler<ListDebtsQuery, IEnumerable<Debt>>
{
    private readonly IDebtRepository _debtRepository;

    public ListDebtsQueryHandler(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<IEnumerable<Debt>> Handle(ListDebtsQuery request, CancellationToken cancellationToken)
    {
        return await _debtRepository.GetByUserIdAsync(request.UserId);
    }
}