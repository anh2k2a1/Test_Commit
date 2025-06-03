namespace Application.CQRS.DTOs
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public bool NotificationEnabled { get; set; }
    }
}
