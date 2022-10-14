using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class UpdateClaimTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountToDate",
                table: "sr_ClaimRecord",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "sr_ClaimRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToDate",
                table: "sr_ClaimRecord");

            migrationBuilder.DropColumn(
                name: "Term",
                table: "sr_ClaimRecord");
        }
    }
}
