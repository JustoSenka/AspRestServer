using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class Owned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Words_WordID",
                table: "Explanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Definitions_DefinitionID",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_DefinitionID",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Explanations",
                table: "Explanations");

            migrationBuilder.DropIndex(
                name: "IX_Explanations_WordID",
                table: "Explanations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions");

            migrationBuilder.DropColumn(
                name: "DefinitionID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Definitions");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Explanations",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "WordID",
                table: "Explanations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordID",
                table: "Definitions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Explanations",
                table: "Explanations",
                columns: new[] { "WordID", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Words_WordID",
                table: "Definitions",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations",
                column: "LanguageToID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Words_WordID",
                table: "Explanations",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Definitions_Words_WordID",
                table: "Definitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations");

            migrationBuilder.DropForeignKey(
                name: "FK_Explanations_Words_WordID",
                table: "Explanations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Explanations",
                table: "Explanations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions");

            migrationBuilder.DropColumn(
                name: "WordID",
                table: "Definitions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Explanations",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "DefinitionID",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WordID",
                table: "Explanations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Definitions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Explanations",
                table: "Explanations",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Definitions",
                table: "Definitions",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_DefinitionID",
                table: "Words",
                column: "DefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Explanations_WordID",
                table: "Explanations",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Languages_LanguageToID",
                table: "Explanations",
                column: "LanguageToID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Explanations_Words_WordID",
                table: "Explanations",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Definitions_DefinitionID",
                table: "Words",
                column: "DefinitionID",
                principalTable: "Definitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
