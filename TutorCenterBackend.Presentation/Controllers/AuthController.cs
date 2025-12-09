using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorCenterBackend.Application.DTOs.Auth.Requests;
using TutorCenterBackend.Application.Interfaces;

namespace TutorCenterBackend.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("send-otp-register")]
        public async Task<IActionResult> SendOtpRegister([FromBody] SendOtpRegisterRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.SendOtpRegisterAsync(dto, ct);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.RegisterAsync(dto, ct);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.LoginAsync(dto, ct);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.RefreshTokenAsync(dto, ct);
            return Ok(result);
        }

        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.LogoutAsync(dto, ct);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.ForgotPasswordAsync(dto, ct);
            return Ok(result);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto, CancellationToken ct = default)
        {
            var result = await _service.ResetPasswordAsync(dto, ct);
            return Ok(result);
        }
    }
}
