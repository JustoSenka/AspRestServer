using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageLearnerData.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageFromID = table.Column<int>(nullable: false),
                    LanguageToID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Books_Language_LanguageFromID",
                        column: x => x.LanguageFromID,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Language_LanguageToID",
                        column: x => x.LanguageToID,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Definition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Definition_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageID = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Word_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Language",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WordID = table.Column<int>(nullable: false),
                    DefinitionID = table.Column<int>(nullable: false),
                    BookID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Translation_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translation_Definition_DefinitionID",
                        column: x => x.DefinitionID,
                        principalTable: "Definition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translation_Word_WordID",
                        column: x => x.WordID,
                        principalTable: "Word",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageFromID",
                table: "Books",
                column: "LanguageFromID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageToID",
                table: "Books",
                column: "LanguageToID");

            migrationBuilder.CreateIndex(
                name: "IX_Definition_LanguageID",
                table: "Definition",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_BookID",
                table: "Translation",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_DefinitionID",
                table: "Translation",
                column: "DefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_WordID",
                table: "Translation",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_Word_LanguageID",
                table: "Word",
                column: "LanguageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Definition");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
