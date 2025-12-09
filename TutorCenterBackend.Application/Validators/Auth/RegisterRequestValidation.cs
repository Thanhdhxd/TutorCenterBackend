using FluentValidation;
using TutorCenterBackend.Application.DTOs.Auth.Requests;

namespace TutorCenterBackend.Application.Validators.Auth
{
    public class RegisterRequestValidation : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc")
                .EmailAddress().WithMessage("Email không hợp lệ");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu là bắt buộc")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự")
                .MaximumLength(50).WithMessage("Mật khẩu không được vượt quá 50 ký tự");
            
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ và tên là bắt buộc")
                .MaximumLength(100).WithMessage("Họ và tên không được vượt quá 100 ký tự");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^(0|\+84)[0-9]{9,10}$").WithMessage("Số điện thoại không hợp lệ. Định dạng: 0xxxxxxxxx hoặc +84xxxxxxxxx");
            
            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("Mã OTP là bắt buộc")
                .Length(6).WithMessage("Mã OTP phải có đúng 6 ký tự");
            
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Xác nhận mật khẩu là bắt buộc")
                .Equal(x => x.Password).WithMessage("Xác nhận mật khẩu không khớp với mật khẩu");
        }
    }
}
