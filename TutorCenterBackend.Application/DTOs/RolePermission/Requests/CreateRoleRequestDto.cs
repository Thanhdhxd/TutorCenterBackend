namespace TutorCenterBackend.Application.DTOs.RolePermission.Requests
{
    public class CreateRoleRequestDto
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
