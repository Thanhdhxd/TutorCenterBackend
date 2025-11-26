using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .Where(p => p.DeletedAt == null)
            .OrderBy(p => p.Module)
            .ThenBy(p => p.PermissionName)
            .ToListAsync();
    }

    public async Task<Permission?> GetByIdAsync(int id)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.PermissionId == id && p.DeletedAt == null);
    }

    public async Task<Permission?> GetByNameAsync(string name)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.PermissionName == name && p.DeletedAt == null);
    }

    public async Task<IEnumerable<Permission>> GetByModuleAsync(string module)
    {
        return await _context.Permissions
            .Where(p => p.Module == module && p.DeletedAt == null)
            .OrderBy(p => p.PermissionName)
            .ToListAsync();
    }

    public async Task<Permission> CreateAsync(Permission permission)
    {
        permission.CreatedAt = DateTime.Now;
        permission.UpdatedAt = DateTime.Now;
        
        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
        
        return permission;
    }

    public async Task<Permission> UpdateAsync(Permission permission)
    {
        permission.UpdatedAt = DateTime.Now;
        
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync();
        
        return permission;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var permission = await GetByIdAsync(id);
        if (permission == null)
            return false;

        // Soft delete
        permission.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Permissions
            .AnyAsync(p => p.PermissionName == name && p.DeletedAt == null);
    }
}
