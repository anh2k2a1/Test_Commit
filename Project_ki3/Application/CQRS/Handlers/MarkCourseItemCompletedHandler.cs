using Application.CQRS.Commands;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Handlers
{
    public class MarkCourseItemCompletedCommandHandler : IRequestHandler<MarkCourseItemCompletedCommand, bool>
    {
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IFinancialCourseRepository _courseRepository;

        public MarkCourseItemCompletedCommandHandler(
            IUserCourseRepository userCourseRepository,
            IFinancialCourseRepository courseRepository)
        {
            _userCourseRepository = userCourseRepository;
            _courseRepository = courseRepository;
        }

        public async Task<bool> Handle(MarkCourseItemCompletedCommand request, CancellationToken cancellationToken)
        {
            // Get the user's purchase record for this course
            var userCourse = await _userCourseRepository.GetByUserAndCourseIdAsync(request.UserId, request.CourseId);
            if (userCourse == null || !userCourse.IsActive)
            {
                throw new Exception("User has not purchased this course or the purchase is inactive");
            }

            // Verify the course item exists
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            var courseItem = course.CourseList.FirstOrDefault(ci => ci.Id == request.CourseItemId);
            if (courseItem == null)
            {
                throw new Exception("Course item not found");
            }

            // Add the item to completed items if not already completed
            if (!userCourse.CompletedItems.Contains(request.CourseItemId))
            {
                userCourse.CompletedItems.Add(request.CourseItemId);
                return await _userCourseRepository.UpdateAsync(userCourse);
            }

            return true; // Already completed
        }
    }
}
