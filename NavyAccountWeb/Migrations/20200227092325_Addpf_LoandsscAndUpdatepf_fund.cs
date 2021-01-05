using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Addpf_LoandsscAndUpdatepf_fund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profitacct",
                table: "pf_Funds",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "opbalance",
                table: "npf_Ledgers",
                type: "decimal(10,3)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "crbalance",
                table: "npf_Ledgers",
                type: "decimal(10,3)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "balpl",
                table: "npf_Ledgers",
                type: "decimal(10,3)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "adbbalance",
                table: "npf_Ledgers",
                type: "decimal(10,3)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "pf_loandisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fundcode = table.Column<int>(nullable: false),
                    loantype = table.Column<int>(nullable: false),
                    principal = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    interest = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    loanpay = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    prvloan = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    extpay = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    loanvar = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    loandiscr = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    loanact = table.Column<string>(nullable: true),
                    intract = table.Column<string>(nullable: true),
                    trusteeact = table.Column<string>(nullable: true),
                    incomeact = table.Column<string>(nullable: true),
                    liabilityact = table.Column<string>(nullable: true),
                    groupcode = table.Column<string>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: false),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_loandisc", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pf_loandisc");

            migrationBuilder.DropColumn(
                name: "profitacct",
                table: "pf_Funds");

            migrationBuilder.AlterColumn<string>(
                name: "opbalance",
                table: "npf_Ledgers",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)");

            migrationBuilder.AlterColumn<string>(
                name: "crbalance",
                table: "npf_Ledgers",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)");

            migrationBuilder.AlterColumn<string>(
                name: "balpl",
                table: "npf_Ledgers",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)");

            migrationBuilder.AlterColumn<string>(
                name: "adbbalance",
                table: "npf_Ledgers",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,3)");
        }
    }
}
