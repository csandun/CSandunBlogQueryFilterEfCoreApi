using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSandunBlogQueryFilterEfCoreApi.Migrations
{
    public partial class Addtodoitem_seeds2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsComplete", "Name" },
                values: new object[] { 3L, true, "Deleted todo item" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
