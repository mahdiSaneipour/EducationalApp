using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EP.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditCourseTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubGroupId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubGroupId",
                table: "Courses",
                column: "SubGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupes_SubGroupId",
                table: "Courses",
                column: "SubGroupId",
                principalTable: "CourseGroupes",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupes_SubGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubGroupId",
                table: "Courses");
        }
    }
}
