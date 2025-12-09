using TutorCenterBackend.Domain.Entities;

namespace TutorCenterBackend.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);
        Task CreateAsync(RefreshToken refreshToken, CancellationToken ct = default);
        Task DeleteAsync(RefreshToken refreshToken, CancellationToken ct = default);
    }
}
