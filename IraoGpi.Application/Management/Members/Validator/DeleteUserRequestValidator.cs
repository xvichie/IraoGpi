using FluentValidation;
using IraoGpi.Application.Management.Members.Requests;

namespace IraoGpi.Application.Management.Members.Validator;

public class DeleteMemberRequestValidator : AbstractValidator<DeleteMemberRequest>
{
    public DeleteMemberRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
    }
}
