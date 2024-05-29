﻿using FluentValidation;
using IraoGpi.Application.Management.Members.Requests;

namespace IraoGpi.Application.Management.Members.Validator;

public class GetMemberRequestValidator : AbstractValidator<GetMemberRequest>
{
    public GetMemberRequestValidator()
    {
        RuleFor(request => request.Id).GreaterThan(0);
    }
}
