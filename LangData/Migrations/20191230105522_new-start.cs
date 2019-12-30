using Microsoft.EntityFrameworkCore.Migrations;

namespace LangData.Migrations
{
    public partial class newstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateText",
                table: "Definitions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Article",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "AlternateText",
                table: "Definitions");
        }
    }
}
