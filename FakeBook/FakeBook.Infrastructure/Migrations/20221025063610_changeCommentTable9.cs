using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class changeCommentTable9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Messages_MessageId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Comments",
                newName: "messageId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_MessageId",
                table: "Comments",
                newName: "IX_Comments_messageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Messages_messageId",
                table: "Comments",
                column: "messageId",
                principalTable: "Messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Messages_messageId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "messageId",
                table: "Comments",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_messageId",
                table: "Comments",
                newName: "IX_Comments_MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Messages_MessageId",
                table: "Comments",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
