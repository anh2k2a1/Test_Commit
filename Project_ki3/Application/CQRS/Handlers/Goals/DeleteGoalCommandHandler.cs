using Application.CQRS.Commands.Goals;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Goals;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IGoalRepository _goalRepository;

    public DeleteGoalCommandHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        return await _goalRepository.DeleteAsync(request.Id);
    }
}   