using MediatR;
using Domain.Models;

namespace Application.CQRS.Commands.FinancialCourses;

public record CreateFinancialCourseCommand(
    string Title,
    string Description,
    decimal Rating,
    int ReviewCount,
    decimal Price,
    decimal Discount,
    string ImageUrl,
    List<string> Tags,
    List<FinancialCourseItem> CourseList,
    bool Lock
) : IRequest<string>;
