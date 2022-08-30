using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Data.Migrations
{
    public partial class UserUtcValueMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UtcMinutes",
                schema: "Identity",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtcMinutes",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);
        }
    }
}
