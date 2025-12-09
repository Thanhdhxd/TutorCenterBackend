using TutorCenterBackend.Application.DTOs.Auth.Requests;
using TutorCenterBackend.Application.DTOs.Auth.Responses;

namespace TutorCenterBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> SendOtpRegisterAsync(SendOtpRegisterRequestDto dto, CancellationToken ct = default);
        Task<string> RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default);
        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken ct = default);
        Task<string> LogoutAsync(LogoutRequestDto dto, CancellationToken ct = default);
        Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto dto, CancellationToken ct = default);
        Task<string> ResetPasswordAsync(ResetPasswordRequestDto dto, CancellationToken ct = default);
    }
}
