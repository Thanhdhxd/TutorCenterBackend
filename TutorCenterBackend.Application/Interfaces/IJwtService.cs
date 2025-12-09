namespace TutorCenterBackend.Application.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(int userId, string userRole);
        Task<string> GenerateRefreshToken(int userId);
        Task<bool> VerifyAccessToken(string token);
        Task<bool> VerifyRefreshToken(string token);
    }
}
