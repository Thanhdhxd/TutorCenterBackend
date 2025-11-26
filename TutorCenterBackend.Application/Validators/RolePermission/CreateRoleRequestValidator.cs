using FluentValidation;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;

namespace TutorCenterBackend.Application.Validators.RolePermission
{
    public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequestDto>
    {
        public CreateRoleRequestValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Tên vai trò không được bỏ trống")
                .MaximumLength(100).WithMessage("Tên vai trò không được vượt quá 100 ký tự")
                .Matches("^[a-zA-Z0-9_\\s]+$").WithMessage("Tên vai trò chỉ được chứa chữ cái, số và dấu gạch dưới");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
