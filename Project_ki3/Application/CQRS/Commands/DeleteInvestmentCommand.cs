using MediatR;

namespace Application.CQRS.Commands;

public record DeleteInvestmentCommand(string Id) : IRequest<bool>;