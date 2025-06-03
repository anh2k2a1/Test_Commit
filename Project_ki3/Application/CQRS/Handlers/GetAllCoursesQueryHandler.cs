using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<FinancialCourse>>
    {
        private readonly IFinancialCourseRepository _courseRepository;

        public GetAllCoursesQueryHandler(IFinancialCourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<FinancialCourse>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _courseRepository.GetAllAsync();
        }
    }
}