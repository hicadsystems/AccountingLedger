using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class UpdateInvestmentRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "pf_InvestRegisters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "pf_InvestRegisters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockId",
                table: "pf_InvestRegisters");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "pf_InvestRegisters");
        }
    }
}
