using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries
{
    public class GetUserCourseProgressQuery : IRequest<UserCourseProgressDto>
    {
        public required string UserId { get; set; }
        public required string CourseId { get; set; }
    }
}