namespace Application.CQRS.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public bool NotificationEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
