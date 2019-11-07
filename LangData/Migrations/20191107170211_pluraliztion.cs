using Microsoft.EntityFrameworkCore.Migrations;

namespace LangData.Migrations
{
    public partial class pluraliztion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Definition_Language_LanguageID",
                table: "Definition");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Definition_DefinitionID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Translation_Word_WordID",
                table: "Translation");

            migrationBuilder.DropForeignKey(
                name: "FK_Word_Books_BookID",
                table: "Word");

            migrationBuilder.DropForeignKey(
                name: "FK_Word_Language_LanguageID",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Word",
                table: "Word");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translation",
                table: "Translation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Definition",
                table: "Definition");

            migrationBuilder.RenameTable(
                name: "Word",
                newName: "Words");

            migrationBuilder.RenameTable(
                name: "Translation",
                newName: "Translations");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Definition",
                newName: "Definitions");

            migrationBuilder.RenameIndex(
                name: "IX_Word_LanguageID",
                table: "Words",
                newName: "IX_Words_LanguageID");

            migrationBuilder.RenameIndex(
                name: "IX_Word_BookID",
                table: "Words",
                newName: "IX_Words_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_Translation_WordID",
                table: "Translations",
                newName: "IX_Translations_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Translation_DefinitionID",
                table: "Translations",
                newName: "IX_Translations_DefinitionID");

            migrationBuilder.RenameIndex(
                name: "IX_Definition_LanguageID",
                table: "Definitions",
                newName: "IX_Definitions_LanguageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translations",
                table: "Translations",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Books_BookID",
                table: "Words",
                column: "BookID",
                principalTable: "Books",
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
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Definitions_DefinitionID",
                table: "Translations");

            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_WordID",
                table: "Translations");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Books_BookID",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Languages_LanguageID",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Translations",
                table: "Translations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "Word");

            migrationBuilder.RenameTable(
                name: "Translations",
                newName: "Translation");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Definitions",
                newName: "Definition");

            migrationBuilder.RenameIndex(
                name: "IX_Words_LanguageID",
                table: "Word",
                newName: "IX_Word_LanguageID");

            migrationBuilder.RenameIndex(
                name: "IX_Words_BookID",
                table: "Word",
                newName: "IX_Word_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_WordID",
                table: "Translation",
                newName: "IX_Translation_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Translations_DefinitionID",
                table: "Translation",
                newName: "IX_Translation_DefinitionID");

            migrationBuilder.RenameIndex(
                name: "IX_Definitions_LanguageID",
                table: "Definition",
                newName: "IX_Definition_LanguageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Word",
                table: "Word",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Translation",
                table: "Translation",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Definition",
                table: "Definition",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Definition_Language_LanguageID",
                table: "Definition",
                column: "LanguageID",
                principalTable: "Language",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Definition_DefinitionID",
                table: "Translation",
                column: "DefinitionID",
                principalTable: "Definition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Translation_Word_WordID",
                table: "Translation",
                column: "WordID",
                principalTable: "Word",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Word_Books_BookID",
                table: "Word",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Word_Language_LanguageID",
                table: "Word",
                column: "LanguageID",
                principalTable: "Language",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
