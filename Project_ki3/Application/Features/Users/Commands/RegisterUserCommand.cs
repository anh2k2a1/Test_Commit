using Domain.Enum;
using Domain.Interfaces.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public record RegisterUserCommand(string Email, string UserName, string Password, string Currency, string Language) : IRequest<User>;
}