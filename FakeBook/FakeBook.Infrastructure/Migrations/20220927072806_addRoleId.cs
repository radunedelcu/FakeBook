using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class addRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Role_RoleId",
                table: "users");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "users",
                newName: "roleId");

            migrationBuilder.RenameIndex(
                name: "IX_users_RoleId",
                table: "users",
                newName: "IX_users_roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Role_roleId",
                table: "users",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Role_roleId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "users",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_users_roleId",
                table: "users",
                newName: "IX_users_RoleId");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Developer" });

            migrationBuilder.AddForeignKey(
                name: "FK_users_Role_RoleId",
                table: "users",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
