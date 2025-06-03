using Application.CQRS.Commands.Goals;
using FluentValidation;

namespace Application.CQRS.Validation;

public class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
{
    public CreateGoalCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Goal title is required.")
            .MaximumLength(100).WithMessage("Goal title must not exceed 100 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Goal type must be either 'Target' or 'Milestone'.");

        RuleFor(x => x.Target)
            .GreaterThan(0).WithMessage("Target amount must be greater than 0.");

        RuleFor(x => x.MilestoneLabels)
            .NotNull().WithMessage("Milestone labels must not be null.")
            .Must(labels => labels.All(label => !string.IsNullOrWhiteSpace(label)))
            .WithMessage("Milestone labels must not contain empty or whitespace values.");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future.")
            .When(x => x.Deadline.HasValue);

        RuleFor(x => x.ReminderAt)
            .LessThan(x => x.Deadline)
            .WithMessage("Reminder must be set before the deadline.")
            .When(x => x.ReminderAt.HasValue && x.Deadline.HasValue);
    }
}
