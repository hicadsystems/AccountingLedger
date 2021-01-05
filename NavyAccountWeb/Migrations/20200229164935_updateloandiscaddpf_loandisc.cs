using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updateloandiscaddpf_loandisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fundcode",
                table: "pf_loandisc");

            migrationBuilder.AlterColumn<string>(
                name: "loantype",
                table: "pf_loandisc",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "loanappno",
                table: "pf_loandisc",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loanappno",
                table: "pf_loandisc");

            migrationBuilder.AlterColumn<int>(
                name: "loantype",
                table: "pf_loandisc",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fundcode",
                table: "pf_loandisc",
                nullable: false,
                defaultValue: 0);
        }
    }
}
