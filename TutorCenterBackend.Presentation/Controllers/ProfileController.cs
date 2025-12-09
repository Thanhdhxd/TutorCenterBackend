using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TutorCenterBackend.Application.DTOs.Profile.Request;
using TutorCenterBackend.Application.DTOs.Profile.Responses;
using TutorCenterBackend.Application.Interfaces;

namespace TutorCenterBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _service.GetMeAsync();
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            var result = await _service.ChangePasswordAsync(dto);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto dto)
        {
            var result = await _service.UpdateProfileAsync(dto);
            return Ok(result);
        }
    }
}
