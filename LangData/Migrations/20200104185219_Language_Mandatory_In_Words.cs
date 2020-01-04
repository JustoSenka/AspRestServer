using Microsoft.EntityFrameworkCore.Migrations;

namespace LangData.Migrations
{
    public partial class Language_Mandatory_In_Words : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "AlternateText",
                table: "Definitions");

            migrationBuilder.RenameColumn(
                name: "Examples",
                table: "Definitions",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Words",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AlternateSpelling",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pronaunciation",
                table: "Words",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Definitions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions",
                column: "LanguageID",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "AlternateSpelling",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Pronaunciation",
                table: "Words");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Definitions",
                newName: "Examples");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Words",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LanguageID",
                table: "Definitions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AlternateText",
                table: "Definitions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions",
                column: "LanguageID",
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
    }
}
