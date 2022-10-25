using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class addComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Quote",
                table: "users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MessageEntityId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageEntityId",
                table: "Messages",
                column: "MessageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageEntityId",
                table: "Messages",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageEntityId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageEntityId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageEntityId",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "Quote",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
