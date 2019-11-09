using Microsoft.EntityFrameworkCore.Migrations;

namespace LangData.Migrations
{
    public partial class removetranslation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Definitions_DefinitionID",
                table: "Translations");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_WordID",
                table: "Translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translations",
                table: "Translations");

            migrationBuilder.RenameTable(
                name: "Translations",
                newName: "Translation");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_WordID",
                table: "Translation",
                newName: "IX_Translation_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_DefinitionID",
                table: "Translation",
                newName: "IX_Translation_DefinitionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translation",
                table: "Translation",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Definitions_DefinitionID",
                table: "Translation",
                column: "DefinitionID",
                principalTable: "Definitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Words_WordID",
                table: "Translation",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Definitions_DefinitionID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Words_WordID",
                table: "Translation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translation",
                table: "Translation");

            migrationBuilder.RenameTable(
                name: "Translation",
                newName: "Translations");

            migrationBuilder.RenameIndex(
                name: "IX_Translation_WordID",
                table: "Translations",
                newName: "IX_Translations_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Translation_DefinitionID",
                table: "Translations",
                newName: "IX_Translations_DefinitionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translations",
                table: "Translations",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Definitions_DefinitionID",
                table: "Translations",
                column: "DefinitionID",
                principalTable: "Definitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Words_WordID",
                table: "Translations",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
