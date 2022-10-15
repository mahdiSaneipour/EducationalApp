using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.Infrastructure.Data.Migrations
{
    public partial class mig_FixRelationsOfRoleTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleId1",
                table: "Roles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_RoleId1",
                table: "Roles",
                column: "RoleId1",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }
    }
}
