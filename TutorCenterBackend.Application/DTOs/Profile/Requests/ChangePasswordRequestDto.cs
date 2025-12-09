namespace TutorCenterBackend.Application.DTOs.Profile.Request
{
    public class ChangePasswordRequestDto
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
