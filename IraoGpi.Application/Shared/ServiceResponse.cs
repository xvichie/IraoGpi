using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Shared;

public class ServiceResponse<TResponse> : IResponse<TResponse>
{
    public TResponse Data { get; set; }

    public Status Status { get; set; }
}
