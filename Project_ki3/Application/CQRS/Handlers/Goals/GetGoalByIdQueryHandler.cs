using Application.CQRS.Queries.Goals;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Goals;

public class GetGoalByIdQueryHandler : IRequestHandler<GetGoalByIdQuery, Goal?>
{
    private readonly IGoalRepository _goalRepository;

    public GetGoalByIdQueryHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<Goal?> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
    {
        return await _goalRepository.GetByIdAsync(request.Id);
    }
}