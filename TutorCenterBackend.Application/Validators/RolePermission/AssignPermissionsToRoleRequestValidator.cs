using FluentValidation;
using TutorCenterBackend.Application.DTOs.RolePermission.Requests;

namespace TutorCenterBackend.Application.Validators.RolePermission
{
    public class AssignPermissionsToRoleRequestValidator : AbstractValidator<AssignPermissionsToRoleRequestDto>
    {
        public AssignPermissionsToRoleRequestValidator()
        {
            RuleFor(x => x.PermissionIds)
                .NotNull().WithMessage("Danh sách quy?n không ???c null")
                .NotEmpty().WithMessage("Ph?i ch?n ít nh?t m?t quy?n");

            RuleForEach(x => x.PermissionIds)
                .GreaterThan(0).WithMessage("ID quy?n ph?i là s? d??ng");
        }
    }
}
