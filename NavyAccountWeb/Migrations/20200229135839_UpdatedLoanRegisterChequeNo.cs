using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class UpdatedLoanRegisterChequeNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                table: "pf_LoanRegisters",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loanAccount",
                table: "pf_LoanRegisters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loanAccount",
                table: "pf_LoanRegisters");

            migrationBuilder.AlterColumn<int>(
                name: "ChequeNo",
                table: "pf_LoanRegisters",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
