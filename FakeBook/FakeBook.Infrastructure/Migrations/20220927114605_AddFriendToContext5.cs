using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class AddFriendToContext5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Friends_FriendEntityId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_FriendEntityId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FriendEntityId",
                table: "users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FriendEntityId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_FriendEntityId",
                table: "users",
                column: "FriendEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Friends_FriendEntityId",
                table: "users",
                column: "FriendEntityId",
                principalTable: "Friends",
                principalColumn: "id");
        }
    }
}
