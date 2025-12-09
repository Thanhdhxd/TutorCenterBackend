using FluentValidation;
using TutorCenterBackend.Application.DTOs.Auth.Requests;

namespace TutorCenterBackend.Application.Validators.Auth
{
    public class ResetPasswordValidation : AbstractValidator<ResetPasswordRequestDto>
    {
        public ResetPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc")
                .EmailAddress().WithMessage("Email không hợp lệ");
            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("Mã OTP là bắt buộc")
                .Length(6).WithMessage("Mã OTP phải có đúng 6 ký tự");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu mới là bắt buộc")
                .MinimumLength(6).WithMessage("Mật khẩu mới phải có ít nhất 6 ký tự")
                .MaximumLength(50).WithMessage("Mật khẩu mới không được vượt quá 50 ký tự");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Xác nhận mật khẩu mới là bắt buộc")
                .Equal(x => x.Password).WithMessage("Xác nhận mật khẩu mới không khớp với mật khẩu mới");
        }
    }
}
