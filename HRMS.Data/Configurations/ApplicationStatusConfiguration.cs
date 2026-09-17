using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HRMS.Models.Entities;

namespace HRMS.Data.Configurations;

public class ApplicationStatusConfiguration : IEntityTypeConfiguration<ApplicationStatus>
{
    public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
    {
        builder.ToTable("ApplicationStatuses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            new ApplicationStatus { Id = 1, Name = "Application Created" },
            new ApplicationStatus { Id = 2, Name = "User Selected" },
            new ApplicationStatus { Id = 3, Name = "Amount Entered" },
            new ApplicationStatus { Id = 4, Name = "Application Submitted" }
        );
    }
}
