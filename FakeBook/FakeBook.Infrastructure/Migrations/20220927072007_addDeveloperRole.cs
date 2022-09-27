using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class addDeveloperRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Developer" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
