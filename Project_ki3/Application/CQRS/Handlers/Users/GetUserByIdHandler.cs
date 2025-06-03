using Application.CQRS.DTOs;
using Application.CQRS.Queries.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _repo;

        public GetUserByIdHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Currency = user.Currency,
                Language = user.Language,
                NotificationEnabled = user.NotificationEnabled,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
