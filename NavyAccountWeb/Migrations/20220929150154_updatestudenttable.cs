using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updatestudenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Guardianid",
                table: "sr_StudentRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Parentid",
                table: "sr_StudentRecord",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guardianid",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "Parentid",
                table: "sr_StudentRecord");
        }
    }
}
