using Application.CQRS.Commands.FinancialCourses;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.FinancialCourses;

public class CreateFinancialCourseCommandHandler : IRequestHandler<CreateFinancialCourseCommand, string>
{
    private readonly IFinancialCourseRepository _repository;

    public CreateFinancialCourseCommandHandler(IFinancialCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateFinancialCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new FinancialCourse
        {
            Title = request.Title,
            Description = request.Description,
            Rating = request.Rating,
            ReviewCount = request.ReviewCount,
            Price = request.Price,
            Discount = request.Discount,
            ImageUrl = request.ImageUrl,
            Tags = request.Tags,
            CourseList = request.CourseList,
            Lock = request.Lock,
        };

        await _repository.CreateAsync(course);
        return course.Id!;
    }
}

