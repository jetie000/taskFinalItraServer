using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalTaskItra.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collectionFields_collections_MyCollectionid",
                table: "collectionFields");

            migrationBuilder.DropForeignKey(
                name: "FK_collections_users_Userid",
                table: "collections");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_items_Itemid",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_itemFields_items_Itemid",
                table: "itemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_items_collections_MyCollectionid",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_likes_items_Itemid",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_items_Itemid",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "Itemid",
                table: "tags",
                newName: "itemid");

            migrationBuilder.RenameIndex(
                name: "IX_tags_Itemid",
                table: "tags",
                newName: "IX_tags_itemid");

            migrationBuilder.RenameColumn(
                name: "Itemid",
                table: "likes",
                newName: "itemid");

            migrationBuilder.RenameIndex(
                name: "IX_likes_Itemid",
                table: "likes",
                newName: "IX_likes_itemid");

            migrationBuilder.RenameColumn(
                name: "MyCollectionid",
                table: "items",
                newName: "myCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_items_MyCollectionid",
                table: "items",
                newName: "IX_items_myCollectionid");

            migrationBuilder.RenameColumn(
                name: "Itemid",
                table: "itemFields",
                newName: "itemid");

            migrationBuilder.RenameIndex(
                name: "IX_itemFields_Itemid",
                table: "itemFields",
                newName: "IX_itemFields_itemid");

            migrationBuilder.RenameColumn(
                name: "Itemid",
                table: "comments",
                newName: "itemid");

            migrationBuilder.RenameIndex(
                name: "IX_comments_Itemid",
                table: "comments",
                newName: "IX_comments_itemid");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "collections",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_collections_Userid",
                table: "collections",
                newName: "IX_collections_userid");

            migrationBuilder.RenameColumn(
                name: "MyCollectionid",
                table: "collectionFields",
                newName: "myCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_collectionFields_MyCollectionid",
                table: "collectionFields",
                newName: "IX_collectionFields_myCollectionid");

            migrationBuilder.AddForeignKey(
                name: "FK_collectionFields_collections_myCollectionid",
                table: "collectionFields",
                column: "myCollectionid",
                principalTable: "collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_collections_users_userid",
                table: "collections",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_items_itemid",
                table: "comments",
                column: "itemid",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itemFields_items_itemid",
                table: "itemFields",
                column: "itemid",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_items_collections_myCollectionid",
                table: "items",
                column: "myCollectionid",
                principalTable: "collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_likes_items_itemid",
                table: "likes",
                column: "itemid",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tags_items_itemid",
                table: "tags",
                column: "itemid",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collectionFields_collections_myCollectionid",
                table: "collectionFields");

            migrationBuilder.DropForeignKey(
                name: "FK_collections_users_userid",
                table: "collections");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_items_itemid",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_itemFields_items_itemid",
                table: "itemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_items_collections_myCollectionid",
                table: "items");

            migrationBuilder.DropForeignKey(
                name: "FK_likes_items_itemid",
                table: "likes");

            migrationBuilder.DropForeignKey(
                name: "FK_tags_items_itemid",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "tags",
                newName: "Itemid");

            migrationBuilder.RenameIndex(
                name: "IX_tags_itemid",
                table: "tags",
                newName: "IX_tags_Itemid");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "likes",
                newName: "Itemid");

            migrationBuilder.RenameIndex(
                name: "IX_likes_itemid",
                table: "likes",
                newName: "IX_likes_Itemid");

            migrationBuilder.RenameColumn(
                name: "myCollectionid",
                table: "items",
                newName: "MyCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_items_myCollectionid",
                table: "items",
                newName: "IX_items_MyCollectionid");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "itemFields",
                newName: "Itemid");

            migrationBuilder.RenameIndex(
                name: "IX_itemFields_itemid",
                table: "itemFields",
                newName: "IX_itemFields_Itemid");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "comments",
                newName: "Itemid");

            migrationBuilder.RenameIndex(
                name: "IX_comments_itemid",
                table: "comments",
                newName: "IX_comments_Itemid");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "collections",
                newName: "Userid");

            migrationBuilder.RenameIndex(
                name: "IX_collections_userid",
                table: "collections",
                newName: "IX_collections_Userid");

            migrationBuilder.RenameColumn(
                name: "myCollectionid",
                table: "collectionFields",
                newName: "MyCollectionid");

            migrationBuilder.RenameIndex(
                name: "IX_collectionFields_myCollectionid",
                table: "collectionFields",
                newName: "IX_collectionFields_MyCollectionid");

            migrationBuilder.AddForeignKey(
                name: "FK_collectionFields_collections_MyCollectionid",
                table: "collectionFields",
                column: "MyCollectionid",
                principalTable: "collections",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_collections_users_Userid",
                table: "collections",
                column: "Userid",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_items_Itemid",
                table: "comments",
                column: "Itemid",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itemFields_items_Itemid",
                table: "itemFields",
                column: "Itemid",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_items_collections_MyCollectionid",
                table: "items",
                column: "MyCollectionid",
                principalTable: "collections",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_likes_items_Itemid",
                table: "likes",
                column: "Itemid",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tags_items_Itemid",
                table: "tags",
                column: "Itemid",
                principalTable: "items",
                principalColumn: "id");
        }
    }
}
