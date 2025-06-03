using Application.CQRS.Commands.FinancialCourses;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.FinancialCourses;

public class DeleteFinancialCourseCommandHandler : IRequestHandler<DeleteFinancialCourseCommand, bool>
{
    private readonly IFinancialCourseRepository _repository;

    public DeleteFinancialCourseCommandHandler(IFinancialCourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteFinancialCourseCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}