using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class BookWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Books_BookID",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_BookID",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Words");

            migrationBuilder.CreateTable(
                name: "BookWord",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    WordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookWord", x => new { x.BookId, x.WordId });
                    table.ForeignKey(
                        name: "FK_BookWord_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookWord_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookWord_WordId",
                table: "BookWord",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookWord");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_BookID",
                table: "Words",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Books_BookID",
                table: "Words",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
