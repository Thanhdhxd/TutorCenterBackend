namespace TutorCenterBackend.Application.DTOs.RolePermission.Responses
{
    public class PermissionResponseDto
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Module { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
