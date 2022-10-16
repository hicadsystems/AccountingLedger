using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class UpdateClaimTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "sr_ClaimRecord",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "sr_ClaimRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "sr_ClaimRecord");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "sr_ClaimRecord");
        }
    }
}
