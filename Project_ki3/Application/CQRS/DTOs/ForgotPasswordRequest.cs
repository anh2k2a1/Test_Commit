namespace Application.CQRS.DTOs
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = default!;
    }
}