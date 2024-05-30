using System.ComponentModel.DataAnnotations;

namespace IraoGpi.Domain.Enums;

public enum StatusCode
{
    [Display(Name = nameof(Success))]
    Success = 0,
    [Display(Name = nameof(Error))]
    Error = 1,

    [Display(Name = nameof(UsernameAlreadyExists))]
    UsernameAlreadyExists,
    [Display(Name = nameof(MemberNotFound))]
    MemberNotFound,
    [Display(Name = nameof(InvalidUserNameOrPassword))]
    InvalidUserNameOrPassword,
    [Display(Name = nameof(CannotCreateMember))]
    CannotCreateMember,
    [Display(Name = nameof(CannotAssignRoleToMember))]
    CannotAssignRoleToMember,

    [Display(Name = nameof(TaskNotFound))]
    TaskNotFound,
}
