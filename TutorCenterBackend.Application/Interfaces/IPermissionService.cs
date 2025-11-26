namespace TutorCenterBackend.Application.Interfaces;

public interface IPermissionService
{
    /// <summary>
    /// Check if user has a specific permission
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="permission">Permission name</param>
    /// <returns>True if user has permission</returns>
    Task<bool> HasPermissionAsync(int userId, string permission);

    /// <summary>
    /// Get all permissions of a user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of permission names</returns>
    Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
}
