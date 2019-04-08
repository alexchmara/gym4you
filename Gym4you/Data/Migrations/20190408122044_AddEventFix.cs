using Microsoft.EntityFrameworkCore.Migrations;

namespace Gym4you.Data.Migrations
{
    public partial class AddEventFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "Events",
                newName: "Amount");
        }
    }
}
