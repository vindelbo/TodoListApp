using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoListApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_TodoLists_TodoListId",
                table: "TodoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItem",
                table: "TodoItem");

            migrationBuilder.RenameTable(
                name: "TodoItem",
                newName: "TodoItems");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItem_TodoListId",
                table: "TodoItems",
                newName: "IX_TodoItems_TodoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "Id");

            migrationBuilder.InsertData(
                table: "TodoLists",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "New job at holion" });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "Completed", "Description", "TodoListId" },
                values: new object[,]
                {
                    { 1, true, "Browse holion.dk", 1 },
                    { 2, true, "Send application", 1 },
                    { 3, false, "Get job", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_TodoLists_TodoListId",
                table: "TodoItems",
                column: "TodoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_TodoLists_TodoListId",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TodoLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "TodoItem");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_TodoListId",
                table: "TodoItem",
                newName: "IX_TodoItem_TodoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItem",
                table: "TodoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_TodoLists_TodoListId",
                table: "TodoItem",
                column: "TodoListId",
                principalTable: "TodoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
