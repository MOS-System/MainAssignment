using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavoriteProductDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteServices_Products",
                table: "FavoriteServices");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteServices_Products",
                table: "FavoriteServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteServices_Products",
                table: "FavoriteServices");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteServices_Products",
                table: "FavoriteServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
