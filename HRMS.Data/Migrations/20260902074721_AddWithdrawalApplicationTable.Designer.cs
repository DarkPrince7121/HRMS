using System;
using HRMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260902074721_AddWithdrawalApplicationTable")]
    partial class AddWithdrawalApplicationTable
    { 
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("HRMS.Models.Entities.ApplicationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationStatuses", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Application Created"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User Selected"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Amount Entered"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Application Submitted"
                        });
                });

            modelBuilder.Entity("HRMS.Models.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("HRMS.Models.Entities.EmployeeSalaryBalance", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<decimal>("AmountBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("EmployeeId");

                    b.ToTable("EmployeeSalaryBalances", (string)null);
                });

            modelBuilder.Entity("HRMS.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("HRMS.Models.Entities.WithdrawalApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("CreatedPersonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastChangeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("SelectedEmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StatusLastChangeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("WithdrawalApplications", (string)null);
                });

            modelBuilder.Entity("HRMS.Models.Entities.Employee", b =>
                {
                    b.HasOne("HRMS.Models.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HRMS.Models.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("HRMS.Models.Entities.Employee", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HRMS.Models.Entities.EmployeeSalaryBalance", b =>
                {
                    b.HasOne("HRMS.Models.Entities.Employee", "Employee")
                        .WithOne()
                        .HasForeignKey("HRMS.Models.Entities.EmployeeSalaryBalance", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRMS.Models.Entities.WithdrawalApplication", b =>
                {
                    b.HasOne("HRMS.Models.Entities.ApplicationStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
