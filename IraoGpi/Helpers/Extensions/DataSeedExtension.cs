using IraoGpi.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IraoGpi.API.Helpers.Extensions;

public static class DataSeedExtension
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
        {
            var role = type.ToString();
            var roleCheck = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
            if (!roleCheck)
            {
                roleManager.CreateAsync(new IdentityRole<int>(role)).GetAwaiter().GetResult();
            }
        }
    }
}
