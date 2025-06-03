using Application.CQRS.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers;

public class DeleteInvestmentCommandHandler : IRequestHandler<DeleteInvestmentCommand, bool>
{
    private readonly IInvestmentRepository _investmentRepository;

    public DeleteInvestmentCommandHandler(IInvestmentRepository investmentRepository)
    {
        _investmentRepository = investmentRepository;
    }

    public async Task<bool> Handle(DeleteInvestmentCommand request, CancellationToken cancellationToken)
    {
        return await _investmentRepository.DeleteAsync(request.Id);
    }
}