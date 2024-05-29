using IraoGpi.Domain.Abstractions.Repositories;
using IraoGpi.Domain.Abstractions.Repository;
using IraoGpi.Infrastructure.Data;
using IraoGpi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using IraoGpi.Domain.Entities;

namespace IraoGpi.Infrastructure;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddIdentity(services);
        AddRepositories(services);
        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseInMemoryDatabase(configuration.GetConnectionString("ConnectionString"))
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning))
                .EnableSensitiveDataLogging();
            options.UseLazyLoadingProxies();
        });
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services
            .AddIdentityCore<Member>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            })
            .AddRoles<IdentityRole<int>>()
            .AddSignInManager()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IMemberRepository, MemberRepository>();
    }
}
