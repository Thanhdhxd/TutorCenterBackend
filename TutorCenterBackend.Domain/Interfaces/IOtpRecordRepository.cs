using TutorCenterBackend.Domain.Entities;

namespace TutorCenterBackend.Domain.Interfaces
{
    public interface IOtpRecordRepository
    {
        Task CreateOtpRecordAsync(OtpRecord otpRecord, CancellationToken ct = default);
        Task<OtpRecord?> GetLatestOtpRecordAsync(string email, string purpose, CancellationToken ct = default);
        Task InvalidateOtpRecordsAsync(string email, string purpose, CancellationToken ct = default);
    }
}
