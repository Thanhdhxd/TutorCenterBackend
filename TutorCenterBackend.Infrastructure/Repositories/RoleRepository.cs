using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .Where(r => r.DeletedAt == null)
            .OrderBy(r => r.RoleName)
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == id && r.DeletedAt == null);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == name && r.DeletedAt == null);
    }

    public async Task<Role?> GetByIdWithPermissionsAsync(int id)
    {
        return await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.RoleId == id && r.DeletedAt == null);
    }

    public async Task<Role> CreateAsync(Role role)
    {
        role.CreatedAt = DateTime.Now;
        role.UpdatedAt = DateTime.Now;
        
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        
        return role;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        role.UpdatedAt = DateTime.Now;
        
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        
        return role;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await GetByIdAsync(id);
        if (role == null)
            return false;

        // Soft delete
        role.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Roles
            .AnyAsync(r => r.RoleName == name && r.DeletedAt == null);
    }

    public async Task<bool> AssignPermissionsAsync(int roleId, List<int> permissionIds)
    {
        var role = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.RoleId == roleId && r.DeletedAt == null);

        if (role == null)
            return false;

        // Clear existing permissions
        role.Permissions.Clear();

        // Add new permissions
        var permissions = await _context.Permissions
            .Where(p => permissionIds.Contains(p.PermissionId) && p.DeletedAt == null)
            .ToListAsync();

        foreach (var permission in permissions)
        {
            role.Permissions.Add(permission);
        }

        role.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> RemovePermissionAsync(int roleId, int permissionId)
    {
        var role = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.RoleId == roleId && r.DeletedAt == null);

        if (role == null)
            return false;

        var permission = role.Permissions.FirstOrDefault(p => p.PermissionId == permissionId);
        if (permission != null)
        {
            role.Permissions.Remove(permission);
            role.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        return true;
    }
}
