using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class Translations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookWord_Words_WordId",
                table: "BookWord");

            migrationBuilder.DropForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropIndex(
                name: "IX_Definitions_LanguageID",
                table: "Definitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookWord",
                table: "BookWord");

            migrationBuilder.DropIndex(
                name: "IX_BookWord_WordId",
                table: "BookWord");

            migrationBuilder.DropColumn(
                name: "LanguageID",
                table: "Definitions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Definitions");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "BookWord");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Words",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pronunciation",
                table: "Words",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                table: "Words",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternateSpelling",
                table: "Words",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefinitionID",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MasterWordID",
                table: "Words",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MasterWordId",
                table: "BookWord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookWord",
                table: "BookWord",
                columns: new[] { "BookId", "MasterWordId" });

            migrationBuilder.CreateTable(
                name: "Explanations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(maxLength: 100, nullable: false),
                    LanguageToID = table.Column<int>(nullable: false),
                    WordID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explanations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Explanations_Languages_LanguageToID",
                        column: x => x.LanguageToID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Explanations_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterWords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterWords", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_DefinitionID",
                table: "Words",
                column: "DefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Words_MasterWordID",
                table: "Words",
                column: "MasterWordID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWord_MasterWordId",
                table: "BookWord",
                column: "MasterWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Explanations_LanguageToID",
                table: "Explanations",
                column: "LanguageToID");

            migrationBuilder.CreateIndex(
                name: "IX_Explanations_WordID",
                table: "Explanations",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWord_MasterWords_MasterWordId",
                table: "BookWord",
                column: "MasterWordId",
                principalTable: "MasterWords",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Definitions_DefinitionID",
                table: "Words",
                column: "DefinitionID",
                principalTable: "Definitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_MasterWords_MasterWordID",
                table: "Words",
                column: "MasterWordID",
                principalTable: "MasterWords",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookWord_MasterWords_MasterWordId",
                table: "BookWord");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Definitions_DefinitionID",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_MasterWords_MasterWordID",
                table: "Words");

            migrationBuilder.DropTable(
                name: "Explanations");

            migrationBuilder.DropTable(
                name: "MasterWords");

            migrationBuilder.DropIndex(
                name: "IX_Words_DefinitionID",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_MasterWordID",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookWord",
                table: "BookWord");

            migrationBuilder.DropIndex(
                name: "IX_BookWord_MasterWordId",
                table: "BookWord");

            migrationBuilder.DropColumn(
                name: "DefinitionID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "MasterWordID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "MasterWordId",
                table: "BookWord");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Pronunciation",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlternateSpelling",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AddColumn<int>(
                name: "LanguageID",
                table: "Definitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Definitions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WordId",
                table: "BookWord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookWord",
                table: "BookWord",
                columns: new[] { "BookId", "WordId" });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefinitionID = table.Column<int>(type: "int", nullable: true),
                    WordID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Translations_Definitions_DefinitionID",
                        column: x => x.DefinitionID,
                        principalTable: "Definitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translations_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_LanguageID",
                table: "Definitions",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_BookWord_WordId",
                table: "BookWord",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_DefinitionID",
                table: "Translations",
                column: "DefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_WordID",
                table: "Translations",
                column: "WordID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWord_Words_WordId",
                table: "BookWord",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Definitions_Languages_LanguageID",
                table: "Definitions",
                column: "LanguageID",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
