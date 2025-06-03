using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries.Users
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>> { }
}
