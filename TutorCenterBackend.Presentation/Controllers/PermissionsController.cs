using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Presentation.Attributes;

namespace TutorCenterBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionManagementService _permissionService;
    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(
        IPermissionManagementService permissionService,
        ILogger<PermissionsController> logger)
    {
        _permissionService = permissionService;
        _logger = logger;
    }

    /// <summary>
    /// Get all permissions (Admin only)
    /// </summary>
    [HttpGet]
    [RequirePermission("permission.view")]
    public async Task<IActionResult> GetAllPermissions()
    {
        try
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(new { success = true, data = permissions });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all permissions");
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get permission by ID (Admin only)
    /// </summary>
    [HttpGet("{id}")]
    [RequirePermission("permission.view")]
    public async Task<IActionResult> GetPermissionById(int id)
    {
        try
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            if (permission == null)
                return NotFound(new { success = false, message = "Permission not found" });

            return Ok(new { success = true, data = permission });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting permission {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get permissions by module (Admin only)
    /// </summary>
    [HttpGet("module/{module}")]
    [RequirePermission("permission.view")]
    public async Task<IActionResult> GetPermissionsByModule(string module)
    {
        try
        {
            var permissions = await _permissionService.GetPermissionsByModuleAsync(module);
            return Ok(new { success = true, data = permissions });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting permissions for module {Module}", module);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Create new permission (Admin only)
    /// </summary>
    [HttpPost]
    [RequirePermission("permission.manage")]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequestDto request)
    {
        try
        {
            var permission = await _permissionService.CreatePermissionAsync(request);
            return CreatedAtAction(
                nameof(GetPermissionById),
                new { id = permission.PermissionId },
                new { success = true, data = permission });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating permission");
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Update permission (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [RequirePermission("permission.manage")]
    public async Task<IActionResult> UpdatePermission(int id, [FromBody] UpdatePermissionRequestDto request)
    {
        try
        {
            var permission = await _permissionService.UpdatePermissionAsync(id, request);
            return Ok(new { success = true, data = permission });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating permission {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Delete permission (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [RequirePermission("permission.manage")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        try
        {
            var result = await _permissionService.DeletePermissionAsync(id);
            if (!result)
                return NotFound(new { success = false, message = "Permission not found" });

            return Ok(new { success = true, message = "Permission deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting permission {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }
}
