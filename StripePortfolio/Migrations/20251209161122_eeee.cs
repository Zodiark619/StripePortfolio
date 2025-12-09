using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StripePortfolio.Migrations
{
    /// <inheritdoc />
    public partial class eeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CardInventories_UserId_CardId",
                table: "CardInventories");

            migrationBuilder.CreateIndex(
                name: "IX_CardInventories_UserId",
                table: "CardInventories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CardInventories_UserId",
                table: "CardInventories");

            migrationBuilder.CreateIndex(
                name: "IX_CardInventories_UserId_CardId",
                table: "CardInventories",
                columns: new[] { "UserId", "CardId" },
                unique: true);
        }
    }
}
