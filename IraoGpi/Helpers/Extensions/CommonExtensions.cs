using System.Security.Claims;

namespace IraoGpi.API.Helpers.Extensions;

public static class CommonExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
