using Microsoft.EntityFrameworkCore.Migrations;

namespace LangData.Migrations
{
    public partial class Pronunciation_Spelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pronaunciation",
                table: "Words",
                newName: "Pronunciation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pronunciation",
                table: "Words",
                newName: "Pronaunciation");
        }
    }
}
