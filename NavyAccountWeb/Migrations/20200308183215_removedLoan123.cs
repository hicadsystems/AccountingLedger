using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class removedLoan123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loan1",
                table: "Npf_ClaimRegisters");

            migrationBuilder.DropColumn(
                name: "loan2",
                table: "Npf_ClaimRegisters");

            migrationBuilder.RenameColumn(
                name: "loan3",
                table: "Npf_ClaimRegisters",
                newName: "loan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loan",
                table: "Npf_ClaimRegisters",
                newName: "loan3");

            migrationBuilder.AddColumn<decimal>(
                name: "loan1",
                table: "Npf_ClaimRegisters",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "loan2",
                table: "Npf_ClaimRegisters",
                nullable: true);
        }
    }
}
