using IraoGpi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Abstractions.Responses;

public interface IResponse<TResponse>
{
    TResponse Data { get; set; }

    Status Status { get; set; }
}
