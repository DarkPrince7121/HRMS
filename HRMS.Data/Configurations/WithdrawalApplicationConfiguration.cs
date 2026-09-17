using HRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Data.Configurations;

public class WithdrawalApplicationConfiguration : IEntityTypeConfiguration<WithdrawalApplication>
{
    public void Configure(EntityTypeBuilder<WithdrawalApplication> builder)
    {
        builder.ToTable("WithdrawalApplications");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Amount)
            .IsRequired()
            .HasColumnType("numeric(18,2)");

        builder.Property(w => w.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(w => w.LastChangeDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(w => w.StatusLastChangeDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(w => w.Status)
            .WithMany()
            .HasForeignKey(w => w.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
