using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsForEmailWhitelist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelists_Tenants",
                table: "EmailWhitelists");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelistSettings_Tenants",
                table: "EmailWhitelistSettings");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "EmailWhitelistSettings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "UX_EmailWhitelistSettings_TenantId",
                table: "EmailWhitelistSettings",
                newName: "UX_EmailWhitelistSettings_UserId");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "EmailWhitelists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "UX_EmailWhitelists_TenantId_Email",
                table: "EmailWhitelists",
                newName: "UX_EmailWhitelists_UserId_Email");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelists_Users",
                table: "EmailWhitelists");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailWhitelistSettings_Users",
                table: "EmailWhitelistSettings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EmailWhitelistSettings",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "UX_EmailWhitelistSettings_UserId",
                table: "EmailWhitelistSettings",
                newName: "UX_EmailWhitelistSettings_TenantId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EmailWhitelists",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "UX_EmailWhitelists_UserId_Email",
                table: "EmailWhitelists",
                newName: "UX_EmailWhitelists_TenantId_Email");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailWhitelists_Tenants",
                table: "EmailWhitelists",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailWhitelistSettings_Tenants",
                table: "EmailWhitelistSettings",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
