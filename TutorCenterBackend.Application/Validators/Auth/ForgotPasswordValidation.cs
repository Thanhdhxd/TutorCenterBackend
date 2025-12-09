using FluentValidation;
using FluentValidation.Validators;
using TutorCenterBackend.Application.DTOs.Auth.Requests;

namespace TutorCenterBackend.Application.Validators.Auth
{
    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordRequestDto>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email là bắt buôc")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email không đúng định dạng");
        }
    }
}
