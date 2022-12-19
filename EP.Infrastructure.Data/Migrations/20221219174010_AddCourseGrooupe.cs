using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.Infrastructure.Data.Migrations
{
    public partial class AddCourseGrooupe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseGroupes",
                columns: table => new
                {
                    CP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupes", x => x.CP_Id);
                    table.ForeignKey(
                        name: "FK_CourseGroupes_CourseGroupes_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CourseGroupes",
                        principalColumn: "CP_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupes_ParentId",
                table: "CourseGroupes",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroupes");
        }
    }
}
