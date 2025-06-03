using Application.CQRS.Queries.FinancialCourses;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers.FinancialCourses;

public class GetFinancialCourseByIdQueryHandler : IRequestHandler<GetFinancialCourseByIdQuery, FinancialCourse?>
{
    private readonly IFinancialCourseRepository _repository;

    public GetFinancialCourseByIdQueryHandler(IFinancialCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<FinancialCourse?> Handle(GetFinancialCourseByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}