using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.Infrastructure.Data.Migrations
{
    public partial class AddWallet2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletTypes_walletTypeTypeId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_walletTypeTypeId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "walletTypeTypeId",
                table: "Wallets");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_TypeId",
                table: "Wallets",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletTypes_TypeId",
                table: "Wallets",
                column: "TypeId",
                principalTable: "WalletTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletTypes_TypeId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_TypeId",
                table: "Wallets");

            migrationBuilder.AddColumn<int>(
                name: "walletTypeTypeId",
                table: "Wallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_walletTypeTypeId",
                table: "Wallets",
                column: "walletTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletTypes_walletTypeTypeId",
                table: "Wallets",
                column: "walletTypeTypeId",
                principalTable: "WalletTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
