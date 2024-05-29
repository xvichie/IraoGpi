using FluentValidation;
using IraoGpi.Application.Management.Tasks.Requests;

namespace IraoGpi.Application.Management.Tasks.Validator;

public class DeleteTaskRequestValidator : AbstractValidator<DeleteTaskRequest>
{
    public DeleteTaskRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
    }
}
