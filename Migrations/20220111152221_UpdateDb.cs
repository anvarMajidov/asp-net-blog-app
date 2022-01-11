using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingSystem.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
