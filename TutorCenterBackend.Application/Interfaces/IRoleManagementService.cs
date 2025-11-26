using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.DTOs.RolePermission.Responses;

namespace TutorCenterBackend.Application.Interfaces;

public interface IRoleManagementService
{
    Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync();
    Task<RoleWithPermissionsResponseDto?> GetRoleByIdAsync(int id);
    Task<RoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request);
    Task<RoleResponseDto> UpdateRoleAsync(int id, UpdateRoleRequestDto request);
    Task<bool> DeleteRoleAsync(int id);
    Task<bool> AssignPermissionsToRoleAsync(AssignPermissionsToRoleRequestDto request);
}
