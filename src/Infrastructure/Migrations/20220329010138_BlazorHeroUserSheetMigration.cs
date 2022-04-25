using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Migrations
{
    public partial class BlazorHeroUserSheetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hr_5min",
                table: "SleepSummary");

            migrationBuilder.DropColumn(
                name: "rmssd_5min",
                table: "SleepSummary");

            migrationBuilder.DropColumn(
                name: "met_1min",
                table: "ActivitySummary");

            migrationBuilder.AddColumn<string>(
                name: "GmailAccount",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OuraSheetId",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OuraSheetUrl",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GmailAccount",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OuraSheetId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OuraSheetUrl",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "hr_5min",
                table: "SleepSummary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rmssd_5min",
                table: "SleepSummary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "met_1min",
                table: "ActivitySummary",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
