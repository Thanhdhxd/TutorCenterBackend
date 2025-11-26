using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.DTOs.RolePermission.Responses;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;

namespace TutorCenterBackend.Application.ServicesImplementation;

public class RoleManagementService : IRoleManagementService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public RoleManagementService(
        IRoleRepository roleRepository,
        IMemoryCache cache,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleResponseDto>>(roles);
    }

    public async Task<RoleWithPermissionsResponseDto?> GetRoleByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdWithPermissionsAsync(id);
        return role != null ? _mapper.Map<RoleWithPermissionsResponseDto>(role) : null;
    }

    public async Task<RoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request)
    {
        // Check if role already exists
        if (await _roleRepository.ExistsAsync(request.RoleName))
        {
            throw new InvalidOperationException($"Role '{request.RoleName}' already exists");
        }

        var role = _mapper.Map<Role>(request);
        var created = await _roleRepository.CreateAsync(role);
        
        // Clear cache
        ClearRoleCache();
        
        return _mapper.Map<RoleResponseDto>(created);
    }

    public async Task<RoleResponseDto> UpdateRoleAsync(int id, UpdateRoleRequestDto request)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {id} not found");
        }

        // Check if new name conflicts with existing
        var existing = await _roleRepository.GetByNameAsync(request.RoleName);
        if (existing != null && existing.RoleId != id)
        {
            throw new InvalidOperationException($"Role '{request.RoleName}' already exists");
        }

        _mapper.Map(request, role);
        var updated = await _roleRepository.UpdateAsync(role);
        
        // Clear cache
        ClearRoleCache();
        
        return _mapper.Map<RoleResponseDto>(updated);
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var result = await _roleRepository.DeleteAsync(id);
        
        if (result)
        {
            // Clear cache
            ClearRoleCache();
        }
        
        return result;
    }

    public async Task<bool> AssignPermissionsToRoleAsync(AssignPermissionsToRoleRequestDto request)
    {
        var result = await _roleRepository.AssignPermissionsAsync(request.RoleId, request.PermissionIds);
        
        if (result)
        {
            // Clear cache for all users with this role
            ClearRoleCache();
        }
        
        return result;
    }

    private void ClearRoleCache()
    {
        // Clear all user role caches
        // In production, consider using a more sophisticated cache invalidation strategy
        _cache.Remove("all_roles");
    }
}
