using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Migrations
{
    public partial class SummaryExportMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "exported_date",
                table: "SleepSummary",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "exported_date",
                table: "ReadinessSummary",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "exported_date",
                table: "ActivitySummary",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exported_date",
                table: "SleepSummary");

            migrationBuilder.DropColumn(
                name: "exported_date",
                table: "ReadinessSummary");

            migrationBuilder.DropColumn(
                name: "exported_date",
                table: "ActivitySummary");

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
    }
}
