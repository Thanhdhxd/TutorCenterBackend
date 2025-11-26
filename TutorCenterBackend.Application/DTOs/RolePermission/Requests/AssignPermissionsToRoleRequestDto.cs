namespace TutorCenterBackend.Application.DTOs.RolePermission.Requests
{
    public class AssignPermissionsToRoleRequestDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = new();
    }
}
