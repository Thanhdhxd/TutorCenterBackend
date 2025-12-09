using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;
using TutorCenterBackend.Infrastructure.DataAccess;

namespace TutorCenterBackend.Infrastructure.Repositories
{
    public class OtpRecordRepository : IOtpRecordRepository
    {
        private readonly AppDbContext _context;

        public OtpRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOtpRecordAsync(OtpRecord otpRecord, CancellationToken ct = default)
        {
            await _context.OtpRecords.AddAsync(otpRecord, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<OtpRecord?> GetLatestOtpRecordAsync(string email, string purpose, CancellationToken ct = default)
        {
            return await _context.OtpRecords
                .Where(o => o.Email == email && o.CodeType == purpose && o.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefaultAsync(ct);
        }

        public async Task InvalidateOtpRecordsAsync(string email, string purpose, CancellationToken ct = default)
        {
            var otpRecords = await _context.OtpRecords
                .Where(o => o.Email == email && o.CodeType == purpose && o.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(ct);

            foreach (var otpRecord in otpRecords)
            {
                otpRecord.ExpiresAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(ct);
        }
    }
}
