using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Application.ServicesImplementation;

namespace TutorCenterBackend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register Application Services (Business Logic Layer)
            services.AddScoped<IRoleManagementService, RoleManagementService>();
            services.AddScoped<IPermissionManagementService, PermissionManagementService>();

            return services;
        }
    }
}
