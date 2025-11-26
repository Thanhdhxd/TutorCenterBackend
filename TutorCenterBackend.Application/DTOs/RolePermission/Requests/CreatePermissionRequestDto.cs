namespace TutorCenterBackend.Application.DTOs.RolePermission.Requests
{
    public class CreatePermissionRequestDto
    {
        public string PermissionName { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Module { get; set; } = null!;
    }
}
