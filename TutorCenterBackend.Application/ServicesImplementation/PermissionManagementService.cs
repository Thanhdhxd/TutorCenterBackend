using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.DTOs.RolePermission.Responses;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;

namespace TutorCenterBackend.Application.ServicesImplementation;

public class PermissionManagementService : IPermissionManagementService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public PermissionManagementService(
        IPermissionRepository permissionRepository,
        IMemoryCache cache,
        IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PermissionResponseDto>> GetAllPermissionsAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
    }

    public async Task<PermissionResponseDto?> GetPermissionByIdAsync(int id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        return permission != null ? _mapper.Map<PermissionResponseDto>(permission) : null;
    }

    public async Task<IEnumerable<PermissionResponseDto>> GetPermissionsByModuleAsync(string module)
    {
        var permissions = await _permissionRepository.GetByModuleAsync(module);
        return _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
    }

    public async Task<PermissionResponseDto> CreatePermissionAsync(CreatePermissionRequestDto request)
    {
        // Check if permission already exists
        if (await _permissionRepository.ExistsAsync(request.PermissionName))
        {
            throw new InvalidOperationException($"Permission '{request.PermissionName}' already exists");
        }

        var permission = _mapper.Map<Permission>(request);
        var created = await _permissionRepository.CreateAsync(permission);
        
        // Clear cache
        ClearPermissionCache();
        
        return _mapper.Map<PermissionResponseDto>(created);
    }

    public async Task<PermissionResponseDto> UpdatePermissionAsync(int id, UpdatePermissionRequestDto request)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission == null)
        {
            throw new KeyNotFoundException($"Permission with ID {id} not found");
        }

        // Check if new name conflicts with existing
        var existing = await _permissionRepository.GetByNameAsync(request.PermissionName);
        if (existing != null && existing.PermissionId != id)
        {
            throw new InvalidOperationException($"Permission '{request.PermissionName}' already exists");
        }

        _mapper.Map(request, permission);
        var updated = await _permissionRepository.UpdateAsync(permission);
        
        // Clear cache
        ClearPermissionCache();
        
        return _mapper.Map<PermissionResponseDto>(updated);
    }

    public async Task<bool> DeletePermissionAsync(int id)
    {
        var result = await _permissionRepository.DeleteAsync(id);
        
        if (result)
        {
            // Clear cache
            ClearPermissionCache();
        }
        
        return result;
    }

    private void ClearPermissionCache()
    {
        // Clear all user permission caches
        // In production, consider using a more sophisticated cache invalidation strategy
        _cache.Remove("all_permissions");
    }
}
