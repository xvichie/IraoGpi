using IraoGpi.Application.Abstractions.Responses;
using IraoGpi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application.Shared;

public static class ResponseHelper
{
    public static IResponse<TResponse> Fail<TResponse>(StatusCode statusCode = StatusCode.Error, string message = null,
        TResponse data = null) where TResponse : class
    {
        var result = new ServiceResponse<TResponse>
        {
            Data = data,
            Status = new Status
            {
                Code = statusCode,
                Message = message ?? statusCode.GetDisplayName()
            }
        };

        return result;
    }

    public static IResponse<EmptyResponse> Fail(StatusCode statusCode = StatusCode.Error, string message = null)
    {
        return Fail<EmptyResponse>(statusCode, message);
    }

    public static IResponse<TResponse> Ok<TResponse>(TResponse data, string message = null) where TResponse : class
    {
        var result = new ServiceResponse<TResponse>
        {
            Data = data,
            Status = new Status
            {
                Code = StatusCode.Success,
                Message = message ?? StatusCode.Success.GetDisplayName()
            }
        };

        return result;
    }

    public static IResponse<EmptyResponse> Ok(string message = null)
    {
        return Ok(default(EmptyResponse), message);
    }
}
