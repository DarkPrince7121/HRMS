using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRMS.Data.Migrations
{
    public partial class AddApplicationStatusTable : Migration
    { 
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "ApplicationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Application Created" },
                    { 2, "User Selected" },
                    { 3, "Amount Entered" },
                    { 4, "Application Submitted" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.DropTable(
                name: "ApplicationStatuses");
        }
    }
}
