using FluentValidation;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;

namespace TutorCenterBackend.Application.Validators.RolePermission
{
    public class UpdatePermissionRequestValidator : AbstractValidator<UpdatePermissionRequestDto>
    {
        public UpdatePermissionRequestValidator()
        {
            RuleFor(x => x.PermissionName)
                .NotEmpty().WithMessage("Tên quyền không được bỏ trống")
                .MaximumLength(100).WithMessage("Tên quyền không được vượt quá 100 ký tự");

            RuleFor(x => x.Path)
                .NotEmpty().WithMessage("Đường dẫn không được bỏ trống")
                .MaximumLength(255).WithMessage("Đường dẫn không được vượt quá 255 ký tự")
                .Must(path => path.StartsWith("/")).WithMessage("Đường dẫn phải bắt đầu bằng /");

            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Phương thứcHTTP không được bỏ trống")
                .Must(method => new[] { "GET", "POST", "PUT", "DELETE", "PATCH" }.Contains(method.ToUpper()))
                .WithMessage("Phương thứcHTTP ph?i là GET, POST, PUT, DELETE hoặc PATCH");

            RuleFor(x => x.Module)
                .NotEmpty().WithMessage("Module không được bỏ trống")
                .MaximumLength(100).WithMessage("Module không được vượt quá 100 ký tự");
        }
    }
}
