using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEmailWhitelistGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelists_Users",
                table: "EmailWhitelists");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelistSettings_Users",
                table: "EmailWhitelistSettings");

            migrationBuilder.DropIndex(
                name: "UX_EmailWhitelistSettings_UserId",
                table: "EmailWhitelistSettings");

            migrationBuilder.DropIndex(
                name: "UX_EmailWhitelists_UserId_Email",
                table: "EmailWhitelists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailWhitelistSettings");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "EmailWhitelists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmailWhitelists");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnabled",
                table: "EmailWhitelistSettings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "UX_EmailWhitelists_Email",
                table: "EmailWhitelists",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_EmailWhitelists_Email",
                table: "EmailWhitelists");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnabled",
                table: "EmailWhitelistSettings",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EmailWhitelistSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddedBy",
                table: "EmailWhitelists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EmailWhitelists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "UX_EmailWhitelistSettings_UserId",
                table: "EmailWhitelistSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_EmailWhitelists_UserId_Email",
                table: "EmailWhitelists",
                columns: new[] { "UserId", "Email" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailWhitelists_Users",
                table: "EmailWhitelists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailWhitelistSettings_Users",
                table: "EmailWhitelistSettings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
