using MediatR;
using Domain.Models;

namespace Application.CQRS.Commands.Goals;

public record CreateGoalCommand(
    string UserId,
    string Title,
    GoalType Type,
    decimal Target,
    List<string> MilestoneLabels,
    DateTime? Deadline,
    DateTime? ReminderAt
) : IRequest<string>;
