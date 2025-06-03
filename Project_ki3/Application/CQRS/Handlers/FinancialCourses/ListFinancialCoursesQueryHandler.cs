using Application.CQRS.Queries.FinancialCourses;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.FinancialCourses;

public class ListFinancialCoursesQueryHandler : IRequestHandler<ListFinancialCoursesQuery, IEnumerable<FinancialCourse>>
{
    private readonly IFinancialCourseRepository _repository;

    public ListFinancialCoursesQueryHandler(IFinancialCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FinancialCourse>> Handle(ListFinancialCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}