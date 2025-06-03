using Application.CQRS.Queries.Goals;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Goals;

public class ListGoalsQueryHandler : IRequestHandler<ListGoalsQuery, IEnumerable<Goal>>
{
    private readonly IGoalRepository _goalRepository;

    public ListGoalsQueryHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<IEnumerable<Goal>> Handle(ListGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _goalRepository.GetByUserIdAsync(request.UserId);
    }
}