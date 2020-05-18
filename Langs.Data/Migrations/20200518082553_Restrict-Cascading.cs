using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class RestrictCascading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words");

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations",
                column: "LanguageToID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words");

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations",
                column: "LanguageToID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
