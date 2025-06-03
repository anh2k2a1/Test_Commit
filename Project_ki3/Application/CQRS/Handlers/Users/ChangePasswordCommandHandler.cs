using Application.CQRS.Commands.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        // Fetch the user by Email
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }

        // Verify the current password
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Current password is incorrect.");
        }

        // Hash the new password
        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        // Update the user's password
        user.PasswordHash = newPasswordHash;
        await _userRepository.UpdateAsync(user);

        return true;
    }
}
