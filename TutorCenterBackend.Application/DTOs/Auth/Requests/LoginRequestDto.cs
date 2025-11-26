namespace TutorCenterBackend.Application.DTOs.Auth.Requests
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
