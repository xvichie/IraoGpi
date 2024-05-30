using IraoGpi.Application.Management.Auth;
using IraoGpi.Application.Management.Members;
using IraoGpi.Application.Management.Tasks;
using IraoGpi.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IraoGpi.Application;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddMapper(services);
        AddServices(services);

        return services;
    }

    private static void AddMapper(IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(typeof(TaskProfile));
        services.AddAutoMapper(typeof(MemberProfile));
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<TaskService>();
        services.AddScoped<MemberService>();
        services.AddScoped<AuthService>();
    }
}
