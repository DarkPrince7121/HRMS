using HRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Data.Configurations;

public class EmployeeSalaryBalanceConfiguration : IEntityTypeConfiguration<EmployeeSalaryBalance>
{
    public void Configure(EntityTypeBuilder<EmployeeSalaryBalance> builder)
    {
        builder.ToTable("EmployeeSalaryBalances");

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.AmountBalance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(e => e.Employee)
            .WithOne()
            .HasForeignKey<EmployeeSalaryBalance>(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
