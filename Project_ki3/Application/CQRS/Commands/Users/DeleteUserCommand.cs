using MediatR;

namespace Application.CQRS.Commands.Users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
}
