namespace TutorCenterBackend.Application.DTOs.RolePermission.Responses
{
    public class RoleWithPermissionsResponseDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public List<PermissionResponseDto> Permissions { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
