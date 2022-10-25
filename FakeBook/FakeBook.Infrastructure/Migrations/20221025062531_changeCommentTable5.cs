using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class changeCommentTable5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Comments",
                newName: "Content");
        }
    }
}
