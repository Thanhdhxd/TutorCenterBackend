using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Presentation.Attributes;

namespace TutorCenterBackend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IRoleManagementService _roleService;
    private readonly ILogger<RolesController> _logger;

    public RolesController(
        IRoleManagementService roleService,
        ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    /// <summary>
    /// Get all roles (Admin only)
    /// </summary>
    [HttpGet]
    [RequirePermission("role.view")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(new { success = true, data = roles });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all roles");
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get role by ID with permissions (Admin only)
    /// </summary>
    [HttpGet("{id}")]
    [RequirePermission("role.view")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        try
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
                return NotFound(new { success = false, message = "Role not found" });

            return Ok(new { success = true, data = role });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting role {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Create new role (Admin only)
    /// </summary>
    [HttpPost]
    [RequirePermission("role.manage")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDto request)
    {
        try
        {
            var role = await _roleService.CreateRoleAsync(request);
            return CreatedAtAction(
                nameof(GetRoleById),
                new { id = role.RoleId },
                new { success = true, data = role });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating role");
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Update role (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [RequirePermission("role.manage")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleRequestDto request)
    {
        try
        {
            var role = await _roleService.UpdateRoleAsync(id, request);
            return Ok(new { success = true, data = role });
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
            _logger.LogError(ex, "Error updating role {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Delete role (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [RequirePermission("role.manage")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        try
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result)
                return NotFound(new { success = false, message = "Role not found" });

            return Ok(new { success = true, message = "Role deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting role {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Assign permissions to role (Admin only)
    /// </summary>
    [HttpPost("{id}/permissions")]
    [RequirePermission("role.manage")]
    public async Task<IActionResult> AssignPermissionsToRole(int id, [FromBody] List<int> permissionIds)
    {
        try
        {
            var request = new AssignPermissionsToRoleRequestDto
            {
                RoleId = id,
                PermissionIds = permissionIds
            };

            var result = await _roleService.AssignPermissionsToRoleAsync(request);
            if (!result)
                return NotFound(new { success = false, message = "Role not found" });

            return Ok(new { success = true, message = "Permissions assigned successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning permissions to role {Id}", id);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }
}
