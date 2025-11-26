using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Presentation.Attributes;

namespace TutorCenterBackend.Presentation.Middlewares;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PermissionMiddleware> _logger;

    public PermissionMiddleware(
        RequestDelegate next,
        ILogger<PermissionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IPermissionService permissionService)
    {
        // Skip public endpoints
        if (IsPublicEndpoint(context))
        {
            await _next(context);
            return;
        }

        // Get userId from claims
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            _logger.LogWarning("Unauthorized access attempt to {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Unauthorized: User not authenticated"
            });
            return;
        }

        // Get endpoint metadata
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        // Check required permission
        var requiredPermission = endpoint.Metadata.GetMetadata<RequirePermissionAttribute>();
        if (requiredPermission != null)
        {
            var hasPermission = await permissionService.HasPermissionAsync(userId, requiredPermission.Permission);

            if (!hasPermission)
            {
                _logger.LogWarning(
                    "User {UserId} attempted to access {Path} without permission '{Permission}'",
                    userId, context.Request.Path, requiredPermission.Permission);

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = $"Forbidden: You don't have the required permission '{requiredPermission.Permission}'"
                });
                return;
            }

            _logger.LogDebug("User {UserId} has permission '{Permission}' for {Path}", 
                userId, requiredPermission.Permission, context.Request.Path);
        }

        await _next(context);
    }

    private static bool IsPublicEndpoint(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
            return false;

        // Check if endpoint has [AllowAnonymous] attribute
        var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
        if (allowAnonymous)
            return true;

        // Allow certain paths without authentication
        var path = context.Request.Path.Value?.ToLower() ?? string.Empty;
        return path.StartsWith("/api/auth/") ||
               path.StartsWith("/swagger") ||
               path.StartsWith("/health") ||
               path.StartsWith("/api/public/");
    }
}
