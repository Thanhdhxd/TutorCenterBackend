using FluentValidation;
using TutorCenterBackend.Application.DTOs.Profile.Request;

namespace TutorCenterBackend.Application.Validators.Profile
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordValidation()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mật khẩu hiện tại là bắt buộc");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu mới là bắt buộc")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Xác nhận mật khẩu là bắt buộc")
                .Equal(x => x.NewPassword).WithMessage("Mật khẩu và xác nhận mật khẩu không khớp");
        }
    }
}
