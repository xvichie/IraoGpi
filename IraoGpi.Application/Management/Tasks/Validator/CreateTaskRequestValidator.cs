using FluentValidation;
using IraoGpi.Application.Management.Tasks.Requests;
using IraoGpi.Domain.Enums;
namespace IraoGpi.Application.Management.Tasks.Validator;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty();
        RuleFor(request => request.Description)
            .NotEmpty();
        RuleFor(request => request.Priority)
            .GreaterThanOrEqualTo(0)
            .Must(priority => Enum.IsDefined(typeof(TaskPriority), priority));
    }
}
