using Application.CQRS.Commands.Goals;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.Goals;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, string>
{
    private readonly IGoalRepository _goalRepository;

    public CreateGoalCommandHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<string> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var reminderAt = request.Deadline.HasValue
            ? request.Deadline.Value.AddHours(-24)
            : (DateTime?)null;

        var goal = new Goal
        {
            UserId = request.UserId,
            Title = request.Title,
            Type = request.Type,
            Progress = 0, // Default progress is 0 for a new goal
            Target = request.Target,
            MilestoneLabels = request.MilestoneLabels,
            Deadline = request.Deadline,
            ReminderAt = reminderAt,
            IsCompleted = false, // Default to not completed
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        await _goalRepository.CreateAsync(goal);
        return goal.Id!;
    }
}

