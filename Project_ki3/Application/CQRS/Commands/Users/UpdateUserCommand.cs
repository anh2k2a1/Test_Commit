using MediatR;

namespace Application.CQRS.Commands.Users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public bool NotificationEnabled { get; set; }
    }
}
