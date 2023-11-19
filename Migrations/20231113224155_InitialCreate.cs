using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    saltedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    access = table.Column<bool>(type: "bit", nullable: false),
                    accessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    joinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    loginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isOnline = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    photoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collections", x => x.id);
                    table.ForeignKey(
                        name: "FK_collections_users_Userid",
                        column: x => x.Userid,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Collectionid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_items_collections_Collectionid",
                        column: x => x.Collectionid,
                        principalTable: "collections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Itemid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.id);
                    table.ForeignKey(
                        name: "FK_comments_items_Itemid",
                        column: x => x.Itemid,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "itemFields",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stringFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stringFieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doubleFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doubleFieldValue = table.Column<double>(type: "float", nullable: true),
                    dateFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateFieldValue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    boolFieldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    boolFieldValue = table.Column<bool>(type: "bit", nullable: true),
                    Itemid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemFields", x => x.id);
                    table.ForeignKey(
                        name: "FK_itemFields_items_Itemid",
                        column: x => x.Itemid,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    isLike = table.Column<bool>(type: "bit", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Itemid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.id);
                    table.ForeignKey(
                        name: "FK_likes_items_Itemid",
                        column: x => x.Itemid,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Itemid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_items_Itemid",
                        column: x => x.Itemid,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_collections_Userid",
                table: "collections",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_comments_Itemid",
                table: "comments",
                column: "Itemid");

            migrationBuilder.CreateIndex(
                name: "IX_itemFields_Itemid",
                table: "itemFields",
                column: "Itemid");

            migrationBuilder.CreateIndex(
                name: "IX_items_Collectionid",
                table: "items",
                column: "Collectionid");

            migrationBuilder.CreateIndex(
                name: "IX_likes_Itemid",
                table: "likes",
                column: "Itemid");

            migrationBuilder.CreateIndex(
                name: "IX_tags_Itemid",
                table: "tags",
                column: "Itemid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "itemFields");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
