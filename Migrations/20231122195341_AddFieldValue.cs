using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fieldValue",
                table: "itemFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fieldValue",
                table: "itemFields");
        }
    }
}
