namespace TutorCenterBackend.Application.DTOs.Auth.Requests
{
    public class ResetPasswordRequestDto : LoginRequestDto
    {
        public string OtpCode { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
