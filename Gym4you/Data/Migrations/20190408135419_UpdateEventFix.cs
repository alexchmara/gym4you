using Microsoft.EntityFrameworkCore.Migrations;

namespace Gym4you.Data.Migrations
{
    public partial class UpdateEventFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Instructors_InstructorId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_InstructorId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "InstructorFK",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_InstructorFK",
                table: "Events",
                column: "InstructorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Instructors_InstructorFK",
                table: "Events",
                column: "InstructorFK",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Instructors_InstructorFK",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_InstructorFK",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "InstructorFK",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_InstructorId",
                table: "Events",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Instructors_InstructorId",
                table: "Events",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
