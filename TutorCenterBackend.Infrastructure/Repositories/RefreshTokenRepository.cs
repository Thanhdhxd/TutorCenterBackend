using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task CreateAsync(RefreshToken refreshToken, CancellationToken ct = default)
        {
            _context.RefreshTokens.Add(refreshToken);
            return _context.SaveChangesAsync(ct);
        }

        public Task DeleteAsync(RefreshToken refreshToken, CancellationToken ct = default)
        {
            _context.RefreshTokens.Remove(refreshToken);
            return _context.SaveChangesAsync(ct);
        }

        public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
            var refreshToken = _context.RefreshTokens
                .FirstOrDefault(rt => rt.Token == token);
            return Task.FromResult(refreshToken);
        }
    }
}
