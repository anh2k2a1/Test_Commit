using Application.CQRS.Commands.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _repo;

        public UpdateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            if (user == null) return false;

            user.UserName = request.UserName;
            user.Currency = request.Currency;
            user.Language = request.Language;
            user.NotificationEnabled = request.NotificationEnabled;
            await _repo.UpdateAsync(user);
            return true;
        }
    }
}
