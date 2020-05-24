using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    NativeLanguageID = table.Column<int>(nullable: true),
                    LearningLanguageID = table.Column<int>(nullable: true),
                    AdditionalLanguageID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Languages_AdditionalLanguageID",
                        column: x => x.AdditionalLanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Languages_LearningLanguageID",
                        column: x => x.LearningLanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Languages_NativeLanguageID",
                        column: x => x.NativeLanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AdditionalLanguageID",
                table: "Accounts",
                column: "AdditionalLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LearningLanguageID",
                table: "Accounts",
                column: "LearningLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_NativeLanguageID",
                table: "Accounts",
                column: "NativeLanguageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
