using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updateclaimtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beneficiary",
                table: "Npf_ClaimRegisters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundTypeID",
                table: "Npf_ClaimRegisters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beneficiary",
                table: "Npf_ClaimRegisters");

            migrationBuilder.DropColumn(
                name: "FundTypeID",
                table: "Npf_ClaimRegisters");
        }
    }
}
