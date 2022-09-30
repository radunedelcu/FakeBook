using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class DbSetImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageEntity_Messages_MessageEntityId",
                table: "ImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageEntity",
                table: "ImageEntity");

            migrationBuilder.RenameTable(
                name: "ImageEntity",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_ImageEntity_MessageEntityId",
                table: "Images",
                newName: "IX_Images_MessageEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Messages_MessageEntityId",
                table: "Images",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Messages_MessageEntityId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Images_MessageEntityId",
                table: "ImageEntity",
                newName: "IX_ImageEntity_MessageEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageEntity",
                table: "ImageEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageEntity_Messages_MessageEntityId",
                table: "ImageEntity",
                column: "MessageEntityId",
                principalTable: "Messages",
                principalColumn: "id");
        }
    }
}
