using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class AddFriendToContext54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Friends_FriendId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_FriendId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Friends",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_users_UserId",
                table: "Friends",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_users_UserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Friends");

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_users_FriendId",
                table: "users",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Friends_FriendId",
                table: "users",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
