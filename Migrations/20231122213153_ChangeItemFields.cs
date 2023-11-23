using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fieldType",
                table: "itemFields");

            migrationBuilder.DropColumn(
                name: "fieldValue",
                table: "itemFields");

            migrationBuilder.AddColumn<bool>(
                name: "boolFieldValue",
                table: "itemFields",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateFieldValue",
                table: "itemFields",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "doubleFieldValue",
                table: "itemFields",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stringFieldValue",
                table: "itemFields",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "boolFieldValue",
                table: "itemFields");

            migrationBuilder.DropColumn(
                name: "dateFieldValue",
                table: "itemFields");

            migrationBuilder.DropColumn(
                name: "doubleFieldValue",
                table: "itemFields");

            migrationBuilder.DropColumn(
                name: "stringFieldValue",
                table: "itemFields");

            migrationBuilder.AddColumn<string>(
                name: "fieldType",
                table: "itemFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fieldValue",
                table: "itemFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
