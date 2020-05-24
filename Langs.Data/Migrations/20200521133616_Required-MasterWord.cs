using Microsoft.EntityFrameworkCore.Migrations;

namespace Langs.Data.Migrations
{
    public partial class RequiredMasterWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MasterWordID",
                table: "Words",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MasterWordID",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
