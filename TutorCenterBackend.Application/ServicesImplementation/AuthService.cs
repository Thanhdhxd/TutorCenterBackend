using Microsoft.Extensions.Options;
using TutorCenterBackend.Application.DTOs.Auth.Requests;
using TutorCenterBackend.Application.DTOs.Auth.Responses;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Application.Options;
using TutorCenterBackend.Domain.Entities;
using TutorCenterBackend.Domain.Interfaces;

namespace TutorCenterBackend.Application.ServicesImplementation
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService _email;
        private readonly IUserRepository _userRepository;
        private readonly IOtpRecordRepository _otpRecordRepository;
        private readonly IHashingService _hashingService;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly OtpSettings _otpSettings;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IEmailService email,
            IUserRepository userRepository,
            IOtpRecordRepository otpRecordRepository,
            IHashingService hashingService,
            IRoleRepository roleRepository,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            IOptions<OtpSettings> otpSettings,
            IOptions<JwtSettings> jwtSettings
        )
        {
            _email = email;
            _userRepository = userRepository;
            _otpRecordRepository = otpRecordRepository;
            _hashingService = hashingService;
            _roleRepository = roleRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _otpSettings = otpSettings.Value;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> SendOtpRegisterAsync(SendOtpRegisterRequestDto dto, CancellationToken ct = default)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email, ct))
            {
                throw new InvalidOperationException("Email đã tồn tại");
            }

            await _otpRecordRepository.InvalidateOtpRecordsAsync(dto.Email, "email_verification", ct);

            var otpCode = GenerateOtpCode();

            var newOtpRecord = new OtpRecord
            {
                Email = dto.Email,
                OtpCode = otpCode,
                CodeType = "email_verification",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_otpSettings.ExpiresMinutes)
            };

            await _otpRecordRepository.CreateOtpRecordAsync(newOtpRecord, ct);
            try { await _email.SendOtpEmailAsync(dto.Email, otpCode, "email_verification", ct); }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Gửi email thất bại: " + ex.Message);
            }

            return "Đã gửi mã OTP đến email của bạn";
        }

        private static string GenerateOtpCode()
        {
            var random = new Random();
            return random.Next(0, 999999).ToString("D6");
        }

        public async Task<string> RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email, ct))
            {
                throw new InvalidOperationException("Email đã tồn tại");
            }
            var otpRecord = await _otpRecordRepository.GetLatestOtpRecordAsync(dto.Email, "email_verification", ct);
            if (otpRecord == null || otpRecord.OtpCode != dto.OtpCode)
            {
                throw new InvalidOperationException("Mã OTP không hợp lệ hoặc đã hết hạn");
            }

            var passwordHashed = await _hashingService.HashPassword(dto.Password);
            var role = await _roleRepository.GetByNameAsync("Student");
            if (role == null)
            {
                throw new InvalidOperationException("Role 'Student' không tồn tại trong hệ thống");
            }

            var newUser = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = passwordHashed,
                PhoneNumber = dto.PhoneNumber,
                RoleId = role.RoleId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.CreateUserAsync(newUser, ct);
            await _otpRecordRepository.InvalidateOtpRecordsAsync(dto.Email, "email_verification", ct);
            return "Đăng ký thành công";
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
        {
            var user = await _userRepository.FindByEmailAsync(dto.Email, ct);
            if (user == null || !user.IsActive)
            {
                throw new InvalidOperationException("Email hoặc mật khẩu không đúng");
            }
            var isPasswordValid = await _hashingService.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new InvalidOperationException("Email hoặc mật khẩu không đúng");
            }
            var userRole = await _roleRepository.GetByIdAsync(user.RoleId);
            var accessToken = await _jwtService.GenerateAccessToken(user.UserId, userRole.RoleName);
            var refreshToken = await _jwtService.GenerateRefreshToken(user.UserId);

            var newRefreshToken = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays)
            };
            await _refreshTokenRepository.CreateAsync(newRefreshToken, ct);
            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken ct = default)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(dto.RefreshToken, ct);
            if (refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Refresh token không hợp lệ hoặc đã hết hạn");
            }
            var isValid = await _jwtService.VerifyRefreshToken(dto.RefreshToken);
            if (!isValid)
            {
                throw new InvalidOperationException("Refresh token không hợp lệ hoặc đã hết hạn");
            }
            var user = await _userRepository.FindByIdAsync(refreshToken.UserId, ct);
            if (user == null || !user.IsActive)
            {
                throw new InvalidOperationException("User không tồn tại hoặc đã bị vô hiệu hóa");
            }
            var userRole = await _roleRepository.GetByIdAsync(user.RoleId);
            var newAccessToken = await _jwtService.GenerateAccessToken(user.UserId, userRole.RoleName);
            var newRefreshTokenValue = await _jwtService.GenerateRefreshToken(user.UserId);

            await _refreshTokenRepository.DeleteAsync(refreshToken, ct);

            var newRefreshToken = new RefreshToken
            {
                UserId = user.UserId,
                Token = newRefreshTokenValue,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays)
            };
            await _refreshTokenRepository.CreateAsync(newRefreshToken, ct);

            return new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenValue
            };
        }

        public async Task<string> LogoutAsync(LogoutRequestDto dto, CancellationToken ct = default)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(dto.RefreshToken, ct);
            if (refreshToken != null)
            {
                await _refreshTokenRepository.DeleteAsync(refreshToken, ct);
            }
            return "Đăng xuất thành công";
        }

        public async Task<string> ForgotPasswordAsync(ForgotPasswordRequestDto dto, CancellationToken ct = default)
        {
            var user = await _userRepository.FindByEmailAsync(dto.Email, ct);
            if (user == null || !user.IsActive)
            {
                throw new InvalidOperationException("Email không tồn tại trong hệ thống");
            }
            await _otpRecordRepository.InvalidateOtpRecordsAsync(dto.Email, "password_reset", ct);
            var otpCode = GenerateOtpCode();
            var newOtpRecord = new OtpRecord
            {
                Email = dto.Email,
                OtpCode = otpCode,
                CodeType = "password_reset",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_otpSettings.ExpiresMinutes)
            };
            await _otpRecordRepository.CreateOtpRecordAsync(newOtpRecord, ct);
            try { await _email.SendOtpEmailAsync(dto.Email, otpCode, "password_reset", ct); }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Gửi email thất bại: " + ex.Message);
            }
            return "Đã gửi mã OTP đến email của bạn";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequestDto dto, CancellationToken ct = default)
        {
            var user = await _userRepository.FindByEmailAsync(dto.Email, ct);
            if (user == null || !user.IsActive)
            {
                throw new InvalidOperationException("Email không tồn tại trong hệ thống");
            }
            var otpRecord = await _otpRecordRepository.GetLatestOtpRecordAsync(dto.Email, "password_reset", ct);
            if (otpRecord == null || otpRecord.OtpCode != dto.OtpCode)
            {
                throw new InvalidOperationException("Mã OTP không hợp lệ hoặc đã hết hạn");
            }
            var newHashedPassword = await _hashingService.HashPassword(dto.Password);
            user.PasswordHash = newHashedPassword;
            await _userRepository.UpdateUserAsync(user, ct);
            await _otpRecordRepository.InvalidateOtpRecordsAsync(dto.Email, "password_reset", ct);
            return "Đặt lại mật khẩu thành công";
        }
    }
}
