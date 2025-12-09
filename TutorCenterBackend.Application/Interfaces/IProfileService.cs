using TutorCenterBackend.Application.DTOs.Profile.Request;
using TutorCenterBackend.Application.DTOs.Profile.Responses;

namespace TutorCenterBackend.Application.Interfaces
{
    public interface IProfileService
    {
        Task<UserResponseDto> GetMeAsync(CancellationToken ct = default);
        Task<UserResponseDto> UpdateProfileAsync(UpdateProfileRequestDto dto, CancellationToken ct = default);
        Task<string> ChangePasswordAsync(ChangePasswordRequestDto dto, CancellationToken ct = default);
    }
}
