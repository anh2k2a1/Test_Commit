using Application.CQRS.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers
{
    public class GetUserCoursesQueryHandler(
        IUserCourseRepository userCourseRepository,
        IFinancialCourseRepository courseRepository)
        : IRequestHandler<GetUserCoursesQuery, IEnumerable<FinancialCourse>>
    {
        public async Task<IEnumerable<FinancialCourse>> Handle(GetUserCoursesQuery request, CancellationToken cancellationToken)
        {
            // Get all user's purchased courses
            var userCourses = await userCourseRepository.GetByUserIdAsync(request.UserId);
            
            // Filter only active purchases
            var activeCourseIds = userCourses
                .Where(uc => uc.IsActive)
                .Select(uc => uc.CourseId)
                .ToList();

            // Get the actual course details for each purchased course
            var courses = new List<FinancialCourse>();
            foreach (var courseId in activeCourseIds)
            {
                var course = await courseRepository.GetByIdAsync(courseId);
                if (course != null)
                {
                    courses.Add(course);
                }
            }

            return courses;
        }
    }
}