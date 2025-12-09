using Microsoft.Extensions.Caching.Memory;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Interfaces;

namespace TutorCenterBackend.Application.ServicesImplementation;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(15);

    public PermissionService(IUserRepository userRepository, IMemoryCache cache)
    {
        _userRepository = userRepository;
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

        var permissions = (await _userRepository.GetUserPermissionsAsync(userId)).ToList();

        _cache.Set(cacheKey, permissions, _cacheExpiration);

        return permissions;
    }
}
