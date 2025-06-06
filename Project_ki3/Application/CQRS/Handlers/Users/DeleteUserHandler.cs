﻿using Application.CQRS.Commands.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRS.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repo;

        public DeleteUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            if (user == null) return false;

            await _repo.DeleteAsync(request.Id);
            return true;
        }
    }
}
