using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    username = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    key_password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    hash_password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
