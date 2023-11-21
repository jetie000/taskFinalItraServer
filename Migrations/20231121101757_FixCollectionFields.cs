using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class FixCollectionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collectionFields_collections_Collectionid",
                table: "collectionFields");

            migrationBuilder.DropForeignKey(
                name: "FK_items_collections_Collectionid",
                table: "items");

            migrationBuilder.RenameColumn(
                name: "Collectionid",
                table: "items",
                newName: "MyCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_items_Collectionid",
                table: "items",
                newName: "IX_items_MyCollectionid");

            migrationBuilder.RenameColumn(
                name: "Collectionid",
                table: "collectionFields",
                newName: "MyCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_collectionFields_Collectionid",
                table: "collectionFields",
                newName: "IX_collectionFields_MyCollectionid");

            migrationBuilder.AlterColumn<string>(
                name: "fieldType",
                table: "collectionFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fieldName",
                table: "collectionFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_collectionFields_collections_MyCollectionid",
                table: "collectionFields",
                column: "MyCollectionid",
                principalTable: "collections",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_collections_MyCollectionid",
                table: "items",
                column: "MyCollectionid",
                principalTable: "collections",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collectionFields_collections_MyCollectionid",
                table: "collectionFields");

            migrationBuilder.DropForeignKey(
                name: "FK_items_collections_MyCollectionid",
                table: "items");

            migrationBuilder.RenameColumn(
                name: "MyCollectionid",
                table: "items",
                newName: "Collectionid");

            migrationBuilder.RenameIndex(
                name: "IX_items_MyCollectionid",
                table: "items",
                newName: "IX_items_Collectionid");

            migrationBuilder.RenameColumn(
                name: "MyCollectionid",
                table: "collectionFields",
                newName: "Collectionid");

            migrationBuilder.RenameIndex(
                name: "IX_collectionFields_MyCollectionid",
                table: "collectionFields",
                newName: "IX_collectionFields_Collectionid");

            migrationBuilder.AlterColumn<string>(
                name: "fieldType",
                table: "collectionFields",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "fieldName",
                table: "collectionFields",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_collectionFields_collections_Collectionid",
                table: "collectionFields",
                column: "Collectionid",
                principalTable: "collections",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_collections_Collectionid",
                table: "items",
                column: "Collectionid",
                principalTable: "collections",
                principalColumn: "id");
        }
    }
}
