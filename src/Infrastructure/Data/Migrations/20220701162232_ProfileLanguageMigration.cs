using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Data.Migrations
{
    public partial class ProfileLanguageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                schema: "Identity",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                schema: "Identity",
                table: "Users");
        }
    }
}
