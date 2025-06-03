using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Queries
{
    public record GetAllCoursesQuery : IRequest<IEnumerable<FinancialCourse>>
    {
        // No parameters needed - returns all courses
    }
}