namespace TutorCenterBackend.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string toEmail, string otpCode, string purpose, CancellationToken ct = default);
    }
}
