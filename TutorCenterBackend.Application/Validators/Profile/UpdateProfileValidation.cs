using FluentValidation;
using TutorCenterBackend.Application.DTOs.Profile.Request;

namespace TutorCenterBackend.Application.Validators.Profile
{
    public class UpdateProfileValidation : AbstractValidator<UpdateProfileRequestDto>
    {
        public UpdateProfileValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ và tên là bắt buộc")
                .MaximumLength(100).WithMessage("Họ và tên không vượt quá 100 ký tự");
            
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại là bắt buộc")
                .Matches(@"^(0|\+84)[0-9]{9,10}$").WithMessage("Số điện thoại không hợp lệ. Định dạng: 0xxxxxxxxx hoặc +84xxxxxxxxx");
        }
    }
}
