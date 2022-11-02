using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSandunBlogQueryFilterEfCoreApi.Migrations
{
    public partial class Addtodoitem_seeds3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3L,
                column: "IsDelete",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 3L,
                column: "IsDelete",
                value: false);
        }
    }
}
