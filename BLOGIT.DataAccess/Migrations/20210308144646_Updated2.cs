using Microsoft.EntityFrameworkCore.Migrations;

namespace BLOGIT.DataAccess.Migrations
{
    public partial class Updated2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserWhoLikedId",
                table: "Likes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserWhoLikedId",
                table: "Likes",
                column: "UserWhoLikedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserWhoLikedId",
                table: "Likes",
                column: "UserWhoLikedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserWhoLikedId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserWhoLikedId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserWhoLikedId",
                table: "Likes");
        }
    }
}
