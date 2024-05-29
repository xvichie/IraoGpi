using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IraoGpi.Domain.Entities;
using IraoGpi.Domain.Enums;
namespace IraoGpi.Infrastructure.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasIndex(u => u.UserName).IsUnique();

        builder.HasMany(u => u.Tasks)
            .WithOne(t => t.Member)
            .HasForeignKey(t => t.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(u => u.EntityStatus == EntityStatus.Active);
    }
}
