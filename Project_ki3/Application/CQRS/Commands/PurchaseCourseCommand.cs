using MediatR;

namespace Application.CQRS.Commands
{
    public record PurchaseCourseCommand : IRequest<string>
    {
        public required string UserId { get; set; }
        public required string CourseId { get; set; }
    }
}