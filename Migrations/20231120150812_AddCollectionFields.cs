using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "collectionFields",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fieldType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Collectionid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collectionFields", x => x.id);
                    table.ForeignKey(
                        name: "FK_collectionFields_collections_Collectionid",
                        column: x => x.Collectionid,
                        principalTable: "collections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_collectionFields_Collectionid",
                table: "collectionFields",
                column: "Collectionid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collectionFields");
        }
    }
}
