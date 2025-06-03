using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers;

public class GetDebtByIdQueryHandler : IRequestHandler<GetDebtByIdQuery, Debt?>
{
    private readonly IDebtRepository _debtRepository;

    public GetDebtByIdQueryHandler(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<Debt?> Handle(GetDebtByIdQuery request, CancellationToken cancellationToken)
    {
        return await _debtRepository.GetByIdAsync(request.Id);
    }
}