using FluentValidation;
using IraoGpi.Application.Management.Auth.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Management.Auth.Validator;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Username).NotEmpty();
        RuleFor(request => request.Password).NotEmpty();
    }
}
