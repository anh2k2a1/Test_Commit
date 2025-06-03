using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.FinancialCourses;

public record GetFinancialCourseByIdQuery(string Id) : IRequest<FinancialCourse?>;
