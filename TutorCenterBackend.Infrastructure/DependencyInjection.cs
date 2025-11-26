using Microsoft.Extensions.DependencyInjection;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Infrastructure.Repositories;
using TutorCenterBackend.Infrastructure.ExternalServices;
using TutorCenterBackend.Domain.Interfaces;

namespace TutorCenterBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register Repositories (Data Access Layer)
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // Register Infrastructure Services
        services.AddScoped<IPermissionService, PermissionService>();

        return services;
    }
}
