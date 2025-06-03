using MediatR;
using Domain.Models;

namespace Application.CQRS.Commands.Goals;

public record UpdateGoalCommand(
    string Id,
    string Title,
    GoalType Type,
    decimal Progress,
    decimal Target,
    List<string> MilestoneLabels,
    DateTime? Deadline,
    DateTime? ReminderAt,
    bool IsCompleted
) : IRequest<bool>;
