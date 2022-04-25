using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Migrations
{
    public partial class SummaryExportMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "exported",
                table: "SleepSummary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "exported",
                table: "ReadinessSummary",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "exported",
                table: "ActivitySummary",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exported",
                table: "SleepSummary");

            migrationBuilder.DropColumn(
                name: "exported",
                table: "ReadinessSummary");

            migrationBuilder.DropColumn(
                name: "exported",
                table: "ActivitySummary");
        }
    }
}
