using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class AddPeriodAndLoanApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "loanappno",
                table: "pf_loandisc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "period",
                table: "pf_loandisc",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loanappno",
                table: "pf_loandisc");

            migrationBuilder.DropColumn(
                name: "period",
                table: "pf_loandisc");
        }
    }
}
