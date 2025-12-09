namespace TutorCenterBackend.Application.DTOs.Auth.Requests
{
    public class RegisterRequestDto : ResetPasswordRequestDto
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
