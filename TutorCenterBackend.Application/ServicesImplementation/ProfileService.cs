using TutorCenterBackend.Application.DTOs.Profile.Request;
using TutorCenterBackend.Application.Interfaces;
using TutorCenterBackend.Domain.Interfaces;
using System.Security.Claims;
using AutoMapper;
using TutorCenterBackend.Application.DTOs.Profile.Responses;
using Microsoft.AspNetCore.Http;

namespace TutorCenterBackend.Application.ServicesImplementation
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHashingService _hashingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(
            IUserRepository userRepository, 
            IMapper mapper,
            IHashingService hashingService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashingService = hashingService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Bạn chưa đăng nhập");
            }
            return userId;
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordRequestDto dto, CancellationToken ct = default)
        {
            var userId = GetCurrentUserId();
            
            var user = await _userRepository.FindByIdAsync(userId, ct);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng");
            }

            var isCurrentPasswordValid = await _hashingService.VerifyPassword(dto.CurrentPassword, user.PasswordHash);
            if (!isCurrentPasswordValid)
            {
                throw new InvalidOperationException("Mật khẩu hiện tại không đúng.");
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {
                throw new InvalidOperationException("Mật khẩu mới và xác nhận mật khẩu không khớp.");
            }

            var newHashedPassword = await _hashingService.HashPassword(dto.NewPassword);
            user.PasswordHash = newHashedPassword;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user, ct);

            return "Đổi mật khẩu thành công.";
        }

        public async Task<UserResponseDto> GetMeAsync(CancellationToken ct = default)
        {
            var userId = GetCurrentUserId();
            
            var user = await _userRepository.FindByIdAsync(userId, ct);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng");
            }
            
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateProfileAsync(UpdateProfileRequestDto dto, CancellationToken ct = default)
        {
            var userId = GetCurrentUserId();
            
            var user = await _userRepository.FindByIdAsync(userId, ct);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng");
            }

            user.FullName = dto.FullName;
            user.PhoneNumber = dto.PhoneNumber;
            
            if (dto.AvatarMediaId.HasValue)
            {
                user.AvatarMediaId = dto.AvatarMediaId.Value;
            }
            
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateUserAsync(user, ct);

            return _mapper.Map<UserResponseDto>(updatedUser);
        }
    }
}
