using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGIT.DataAccess.Migrations
{
    public partial class newUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                table: "Comments",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImage",
                table: "Comments");
        }
    }
}
