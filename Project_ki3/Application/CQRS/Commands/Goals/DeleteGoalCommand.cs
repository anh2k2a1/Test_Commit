using MediatR;

namespace Application.CQRS.Commands.Goals;

public record DeleteGoalCommand(string Id) : IRequest<bool>;
