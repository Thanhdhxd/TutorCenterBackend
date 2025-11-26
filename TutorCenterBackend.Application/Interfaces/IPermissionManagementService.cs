using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.DTOs.RolePermission.Responses;

namespace TutorCenterBackend.Application.Interfaces;

public interface IPermissionManagementService
{
    Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync();
    Task<PermissionResponseDto?> GetPermissionByIdAsync(int id);
    Task<IEnumerable<PermissionResponseDto>> GetPermissionsByModuleAsync(string module);
    Task<PermissionResponseDto> CreatePermissionAsync(CreatePermissionRequestDto request);
    Task<PermissionResponseDto> UpdatePermissionAsync(int id, UpdatePermissionRequestDto request);
    Task<bool> DeletePermissionAsync(int id);
}
