using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class Book_WordCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordCount",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WordCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
