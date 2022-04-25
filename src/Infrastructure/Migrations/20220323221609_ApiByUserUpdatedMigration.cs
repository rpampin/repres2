using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repres.Infrastructure.Migrations
{
    public partial class ApiByUserUpdatedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "ApiByUsers",
                newName: "RefreshExpiryDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessExpiryDate",
                table: "ApiByUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "ApiByUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessExpiryDate",
                table: "ApiByUsers");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "ApiByUsers");

            migrationBuilder.RenameColumn(
                name: "RefreshExpiryDate",
                table: "ApiByUsers",
                newName: "ExpiryDate");
        }
    }
}
