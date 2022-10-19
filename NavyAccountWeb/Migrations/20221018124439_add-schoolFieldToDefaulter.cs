using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addschoolFieldToDefaulter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "sr_Defaulter",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "sr_Defaulter");
        }
    }
}
