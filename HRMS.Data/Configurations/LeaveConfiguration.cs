using HRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Data.Configurations;

public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.ToTable("Leaves");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.LeaveType).IsRequired().HasMaxLength(50);
        builder.Property(l => l.Reason).IsRequired().HasMaxLength(500);
        builder.Property(l => l.Status).IsRequired().HasMaxLength(20);

        builder.HasOne(l => l.Employee)
            .WithMany()
            .HasForeignKey(l => l.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}