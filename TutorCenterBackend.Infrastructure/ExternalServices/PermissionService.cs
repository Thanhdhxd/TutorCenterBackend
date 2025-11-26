using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.ExternalServices;

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(15);

    public PermissionService(AppDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<bool> HasPermissionAsync(int userId, string permission)
    {
        var permissions = await GetUserPermissionsAsync(userId);
        return permissions.Contains(permission, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId)
    {
        var cacheKey = $"user_permissions_{userId}";

        if (_cache.TryGetValue(cacheKey, out List<string>? cachedPermissions) && cachedPermissions != null)
        {
            return cachedPermissions;
        }

        // Query permissions from database through user's role
        var permissions = await _context.Users
            .Where(u => u.UserId == userId && u.IsActive)
            .Select(u => u.Role)
            .SelectMany(r => r.Permissions)
            .Select(p => p.PermissionName)
            .Distinct()
            .ToListAsync();

        // Cache the result
        _cache.Set(cacheKey, permissions, _cacheExpiration);

        return permissions;
    }
}
