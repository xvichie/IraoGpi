using IraoGpi.Domain.Abstractions.UnitOfWork;
using IraoGpi.Domain.Entities;
using IraoGpi.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace IraoGpi.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<Member, IdentityRole<int>, int, IdentityUserClaim<int>,
        IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>,
    IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //TODO change to dynamic
        builder.ApplyConfiguration(new TaskConfiguration());
        builder.ApplyConfiguration(new MemberConfiguration());

        base.OnModelCreating(builder);
    }
}
