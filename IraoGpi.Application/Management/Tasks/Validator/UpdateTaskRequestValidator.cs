using FluentValidation;
using IraoGpi.Application.Management.Tasks.Requests;
using IraoGpi.Domain.Enums;

namespace IraoGpi.Application.Management.Tasks.Validator;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
        RuleFor(request => request.Title).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Priority)
            .GreaterThanOrEqualTo(0)
            .Must(priority => Enum.IsDefined(typeof(TaskPriority), priority));
    }
}
