using IraoGpi.Application.Management.Members.Requests;
using IraoGpi.Application.Shared;
using IraoGpi.Domain.Abstractions.Repository;
using IraoGpi.Domain.Entities;
using IraoGpi.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace IraoGpi.API.Helpers.Extensions;

public static class DataSeedExtension
{
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {

        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Member>>();
        var memberRepository = scope.ServiceProvider.GetRequiredService<IMemberRepository>();

        foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
        {
            var role = type.ToString();
            var roleCheck = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
            if (!roleCheck)
            {
                roleManager.CreateAsync(new IdentityRole<int>(role)).GetAwaiter().GetResult();
            }
        }

        var userMember = await memberRepository.GetByUserName("User");
        if (userMember == null)
        {
            userMember = Member.Create("UserFirstName", "UserLastName", "User");

            var result = await userManager.CreateAsync(userMember, "User12.");
            if (!result.Succeeded)
            {
                return;
            }

            var userRole = Enum.GetName(typeof(MemberType), MemberType.User);
            var roleAdded = await userManager.AddToRoleAsync(userMember, userRole);
            if (!roleAdded.Succeeded)
            {
                return;
            }



        }

        var supportMember = await memberRepository.GetByUserName("Support");
        if (supportMember == null)
        {
            supportMember = Member.Create("SupportFirstName", "SupportLastName", "Support");

            var result = await userManager.CreateAsync(supportMember, "Support12.");
            if (!result.Succeeded)
            {
                return;
            }

            var supportRole = Enum.GetName(typeof(MemberType), MemberType.Support);
            var roleAdded = await userManager.AddToRoleAsync(supportMember, supportRole);
            if (!roleAdded.Succeeded)
            {
                return;
            }
        }
    }
}
