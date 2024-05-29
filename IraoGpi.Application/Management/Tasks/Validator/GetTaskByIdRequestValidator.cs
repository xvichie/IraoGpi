using FluentValidation;
using IraoGpi.Application.Management.Tasks.Requests;

namespace IraoGpi.Application.Management.Tasks.Validator;

public class GetTaskByIdRequestValidator : AbstractValidator<GetTaskByIdRequest>
{
    public GetTaskByIdRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
    }
}
