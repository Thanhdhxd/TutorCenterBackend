using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Resend;
using TutorCenterBackend.Application.Interfaces;

namespace TutorCenterBackend.Infrastructure.ExternalServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly IResend _resend;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger, IResend resend)
        {
            _configuration = configuration;
            _logger = logger;
            _resend = resend;
        }

        public async Task SendOtpEmailAsync(string toEmail, string otpCode, string purpose, CancellationToken ct = default)
        {
            var fromEmail = _configuration["Resend:FromEmail"];
            var fromName = _configuration["Resend:FromName"];

            var subject = purpose == "email_verification"
                ? "Mã xác thực đăng ký tài khoản"
                : "Mã xác thực đặt lại mật khẩu";

            var htmlContent = $@"
                <html>
                <body>
                    <h2>{subject}</h2>
                    <p>Mã OTP của bạn là: <strong style='font-size: 24px;'>{otpCode}</strong></p>
                    <p>Mã này sẽ hết hạn sau 5 phút.</p>
                    <p>Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email này.</p>
                </body>
                </html>
            ";

            var message = new EmailMessage
            {
                From = $"{fromName} <{fromEmail}>",
                To = toEmail,
                Subject = subject,
                HtmlBody = htmlContent
            };

            try
            {
                var response = await _resend.EmailSendAsync(message, ct);

                _logger.LogInformation($"OTP email sent successfully to {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending OTP email to {toEmail}");
                throw;
            }
        }
    }
}
