using MediatR;

namespace Application.CQRS.Commands
{
    public record MarkCourseItemCompletedCommand : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string CourseId { get; set; }
        public required int CourseItemId { get; set; }
    }
}