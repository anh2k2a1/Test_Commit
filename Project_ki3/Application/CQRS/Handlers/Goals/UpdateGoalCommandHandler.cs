using Application.CQRS.Commands.Goals;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Goals;

public class UpdateGoalCommandHandler : IRequestHandler<UpdateGoalCommand, bool>
{
    private readonly IGoalRepository _goalRepository;

    public UpdateGoalCommandHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<bool> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await _goalRepository.GetByIdAsync(request.Id);
        if (goal == null) return false;

        // Update fields based on the new Goal model
        goal.Title = request.Title;
        goal.Type = request.Type;
        goal.Progress = request.Progress;
        goal.Target = request.Target;
        goal.MilestoneLabels = request.MilestoneLabels;
        goal.Deadline = request.Deadline;

        // Set ReminderAt to 24 hours before the Deadline, if Deadline is provided
        goal.ReminderAt = request.Deadline.HasValue
            ? request.Deadline.Value.AddHours(-24)
            : null;

        goal.IsCompleted = request.IsCompleted;
        goal.UpdatedAt = DateTime.UtcNow; // Set the updated timestamp

        return await _goalRepository.UpdateAsync(goal);
    }
}
