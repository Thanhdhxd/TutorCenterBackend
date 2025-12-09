using FluentValidation;
using TutorCenterBackend.Application.DTOs.Auth.Requests;

namespace TutorCenterBackend.Application.Validators.Auth
{
    public class SendOtpRegisterValidation : AbstractValidator<SendOtpRegisterRequestDto>
    {
        public SendOtpRegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buộc")
                .EmailAddress().WithMessage("Email không đúng định dạng");
        }
    }
}
