using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserAndAuditLogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "AuditLogs",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "SigninMethod",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "AuditLogs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SigninMethod",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AuditLogs",
                newName: "UserEmail");
        }
    }
}
