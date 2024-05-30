using FluentValidation;
using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Domain.Enums;

namespace IraoGpi.Application.Management.Members.Validator;

public class UpdateMemberRequestValidator : AbstractValidator<UpdateMemberRequest>
{
    public UpdateMemberRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
        RuleFor(request => request.FirstName).NotEmpty();
        RuleFor(request => request.LastName).NotEmpty();
        //RuleFor(request => request.UserName).NotEmpty();
        RuleFor(request => request.Password).NotEmpty()
            .Must(password => password.Length > 4);
        RuleFor(request => request.Type).Must(type => Enum.IsDefined(typeof(MemberType), type));
    }
}
