using IraoGpi.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = IraoGpi.Domain.Entities.Task;
namespace IraoGpi.Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasOne(t => t.Member)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(t => t.EntityStatus == EntityStatus.Active);
    }
}
