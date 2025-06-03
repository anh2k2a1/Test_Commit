using Application.CQRS.Commands.Users;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.CQRS.Handlers.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCryptNet.HashPassword(request.Password);
            var user = new Domain.Models.User
            {
                Email = request.Email,
                UserName = request.UserName,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateAsync(user); 
            return user.Id;
        }
    }
}
