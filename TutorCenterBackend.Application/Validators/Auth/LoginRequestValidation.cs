using FluentValidation;
using TutorCenterBackend.Application.DTOs.Auth.Requests;

namespace TutorCenterBackend.Application.Validators.Auth
{
    public class LoginRequestValidation : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buôc")
                .EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu là bắt buộc")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
        }
    }
}
