using Application.CQRS.Queries;
using Domain.Interfaces;
using MediatR;
using Application.CQRS.DTOs;

namespace Application.CQRS.Handlers
{
    public class GetUserCourseProgressQueryHandler(
        IUserCourseRepository userCourseRepository,
        IFinancialCourseRepository courseRepository)
        : IRequestHandler<GetUserCourseProgressQuery, UserCourseProgressDto>
    {
        public async Task<UserCourseProgressDto> Handle(GetUserCourseProgressQuery request, CancellationToken cancellationToken)
        {
            // Get the user's purchase record for this course
            var userCourse = await userCourseRepository.GetByUserAndCourseIdAsync(request.UserId, request.CourseId);
            if (userCourse == null || !userCourse.IsActive)
            {
                throw new Exception("User has not purchased this course or the purchase is inactive");
            }

            // Get the course details
            var course = await courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            // Create the progress DTO
            var progressDto = new UserCourseProgressDto
            {
                CourseId = course.Id!,
                CourseTitle = course.Title,
                TotalItems = course.CourseList.Count,
                CompletedItems = userCourse.CompletedItems.Count,
                ItemsProgress = course.CourseList.Select(item => new CourseItemProgressDto
                {
                    ItemId = item.Id,
                    Title = item.Title,
                    IsCompleted = userCourse.CompletedItems.Contains(item.Id)
                }).ToList()
            };

            // Calculate progress percentage
            progressDto.ProgressPercentage = progressDto.TotalItems > 0
                ? Math.Round((decimal)progressDto.CompletedItems / progressDto.TotalItems * 100, 2)
                : 0;

            return progressDto;
        }
    }
}
