using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.CQRS.Queries
{
    public record GetUserCoursesQuery : IRequest<IEnumerable<FinancialCourse>>
    {
        public required string UserId { get; set; }
    }
}