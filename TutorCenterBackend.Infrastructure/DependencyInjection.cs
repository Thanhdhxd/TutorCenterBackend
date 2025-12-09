using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Application.Options;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.ExternalServices;
using TutorCenterBackend.Infrastructure.Repositories;

namespace TutorCenterBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Repositories (Data Access Layer)
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpRecordRepository, OtpRecordRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Register HttpClient for Resend
        services.AddHttpClient<IResend, ResendClient>();
        
        // Register Resend client
        services.AddOptions<ResendClientOptions>()
            .Configure(o => o.ApiToken = configuration["Resend:ApiKey"]);

        // Register Email Service
        services.AddScoped<IEmailService, EmailService>();

        // Register OTP Settings
        services.Configure<OtpSettings>(configuration.GetSection("Otp"));

        // Register JWT Settings
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        // Register JWT Service
        services.AddScoped<IJwtService, JwtService>();

        // Register Hashing Service
        services.AddScoped<IHashingService, HashingService>();

        return services;
    }
}
