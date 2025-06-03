using Application.CQRS.DTOs;
using Application.CQRS.Queries.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _repo;

        public GetAllUsersHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                Currency = u.Currency,
                Language = u.Language,
                NotificationEnabled = u.NotificationEnabled,
                CreatedAt = u.CreatedAt
            });
        }
    }
}
