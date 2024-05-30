using FluentValidation;
using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Domain.Enums;

namespace IraoGpi.Application.Management.Members.Validator;

public class CreateMemberRequestValidator : AbstractValidator<CreateMemberRequest>
{
    public CreateMemberRequestValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        RuleFor(request => request.UserName).NotEmpty();
        RuleFor(request => request.Password).NotEmpty()
            .Must(password => password.Length > 4);
        RuleFor(request => request.Type).Must(type => Enum.IsDefined(typeof(MemberType), type));
    }
}
