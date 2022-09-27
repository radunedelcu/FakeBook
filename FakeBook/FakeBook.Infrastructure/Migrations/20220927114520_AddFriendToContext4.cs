using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class AddFriendToContext4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_users_User1Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_users_User2Id",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_User1Id",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_User2Id",
                table: "Friends");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User1Id",
                table: "Friends",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User2Id",
                table: "Friends",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_users_User1Id",
                table: "Friends",
                column: "User1Id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_users_User2Id",
                table: "Friends",
                column: "User2Id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
