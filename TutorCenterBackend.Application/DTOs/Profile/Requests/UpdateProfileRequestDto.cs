namespace TutorCenterBackend.Application.DTOs.Profile.Request
{
    public class UpdateProfileRequestDto
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? AvatarMediaId { get; set; }
    }
}
