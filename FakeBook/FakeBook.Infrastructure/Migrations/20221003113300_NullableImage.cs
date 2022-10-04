using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class NullableImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Messages_MessageEntityId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_MessageEntityId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MessageEntityId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ImageId",
                table: "Messages",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Images_ImageId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ImageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageEntityId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_MessageEntityId",
                table: "Images",
                column: "MessageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Messages_MessageEntityId",
                table: "Images",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "id");
        }
    }
}
