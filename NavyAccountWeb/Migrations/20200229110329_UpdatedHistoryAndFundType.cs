using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class UpdatedHistoryAndFundType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "jvno",
                table: "pf_Funds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "receiptno",
                table: "pf_Funds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "refno",
                table: "npf_Histories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jvno",
                table: "pf_Funds");

            migrationBuilder.DropColumn(
                name: "receiptno",
                table: "pf_Funds");

            migrationBuilder.DropColumn(
                name: "refno",
                table: "npf_Histories");
        }
    }
}
