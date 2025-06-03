using Application.CQRS.Commands.FinancialCourses;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.FinancialCourses;

public class UpdateFinancialCourseCommandHandler : IRequestHandler<UpdateFinancialCourseCommand, bool>
{
    private readonly IFinancialCourseRepository _repository;

    public UpdateFinancialCourseCommandHandler(IFinancialCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateFinancialCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetByIdAsync(request.Id);
        if (course == null) return false;

        // Update fields based on the new structure
        course.Title = request.Title;
        course.Description = request.Description;
        course.Rating = request.Rating;
        course.ReviewCount = request.ReviewCount;
        course.Price = request.Price;
        course.Discount = request.Discount;
        course.ImageUrl = request.ImageUrl;
        course.Tags = request.Tags;
        course.CourseList = request.CourseList;
        course.Lock = request.Lock;

        return await _repository.UpdateAsync(course);
    }
}

