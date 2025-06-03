using MediatR;

namespace Application.CQRS.Commands.FinancialCourses;

public record DeleteFinancialCourseCommand(string Id) : IRequest<bool>;