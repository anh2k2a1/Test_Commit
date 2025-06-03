using Domain.Models;
using MediatR;

namespace Application.CQRS.Queries.FinancialCourses;

public record ListFinancialCoursesQuery() : IRequest<IEnumerable<FinancialCourse>>;