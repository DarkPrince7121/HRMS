using HRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Data.Configurations;

public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.ToTable("Payrolls");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.GrossPay).HasPrecision(18, 2);
        builder.Property(p => p.Deductions).HasPrecision(18, 2);
        builder.Property(p => p.NetPay).HasPrecision(18, 2);
        builder.Property(p => p.Status).IsRequired().HasMaxLength(20);

        builder.HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}