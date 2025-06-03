using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries.Users
{
    public class GetUserByIdQuery : IRequest<UserDto?>
    {
        public string Id { get; set; }
        public GetUserByIdQuery(string id) => Id = id;
    }
}
