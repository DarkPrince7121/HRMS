using HRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Data.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Message).IsRequired().HasMaxLength(500);
        builder.Property(n => n.Type).IsRequired().HasMaxLength(50);
        builder.HasOne(n => n.User).WithMany().HasForeignKey(n => n.UserId);
    }
}