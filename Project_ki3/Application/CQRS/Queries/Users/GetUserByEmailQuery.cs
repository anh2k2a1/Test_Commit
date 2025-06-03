using Application.CQRS.DTOs;
using MediatR;

namespace Application.CQRS.Queries.Users
{
    public class GetUserByEmailQuery : IRequest<UserDto?>
    {
        public string Email { get; set; }
        public GetUserByEmailQuery(string email) => Email = email;
    }
}
