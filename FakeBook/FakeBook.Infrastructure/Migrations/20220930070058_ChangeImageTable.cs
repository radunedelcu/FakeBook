using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeBook.Infrastructure.Migrations
{
    public partial class ChangeImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                table: "ImageEntity");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "ImageEntity");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "ImageEntity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ImageEntity",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DataFiles",
                table: "ImageEntity",
                newName: "DataFile");

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "ImageEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ImageEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ImageEntity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ImageEntity",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataFile",
                table: "ImageEntity",
                newName: "DataFiles");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "ImageEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "ImageEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "ImageEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "ImageEntity",
                type: "datetime2",
                nullable: true);
        }
    }
}
