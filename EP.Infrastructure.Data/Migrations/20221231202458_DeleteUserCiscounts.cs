using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUserCiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "UserDiscounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDiscounts_DiscountId",
                table: "UserDiscounts",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDiscounts_Discount_DiscountId",
                table: "UserDiscounts",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId");
        }
    }
}
