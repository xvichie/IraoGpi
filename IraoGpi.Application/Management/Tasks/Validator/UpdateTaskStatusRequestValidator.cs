using FluentValidation;
using IraoGpi.Application.Management.Tasks.Requests;

namespace IraoGpi.Application.Management.Tasks.Validator;

public class UpdateTaskStatusRequestValidator : AbstractValidator<UpdateTaskStatusRequest>
{
    public UpdateTaskStatusRequestValidator()
    {
        RuleFor(request => request.Status)
            .GreaterThanOrEqualTo(0)
            .Must(status => Enum.IsDefined(typeof(TaskStatus), status));
    }
}
