using AutoMapper;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;
using TutorCenterBackend.Application.DTOs.RolePermission.Responses;
using TutorCenterBackend.Domain.Entities;

namespace TutorCenterBackend.Application.Mappings
{
    public class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            // Permission mappings
            CreateMap<CreatePermissionRequestDto, Permission>()
                .ForMember(dest => dest.PermissionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<UpdatePermissionRequestDto, Permission>()
                .ForMember(dest => dest.PermissionId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<Permission, PermissionResponseDto>();
        }
    }
}
