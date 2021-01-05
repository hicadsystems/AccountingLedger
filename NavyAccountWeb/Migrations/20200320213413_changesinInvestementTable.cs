using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class changesinInvestementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "companyid",
                table: "pf_InvestRegisters");

            migrationBuilder.AddColumn<string>(
                name: "company",
                table: "pf_InvestRegisters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "pf_InvestRegisters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "company",
                table: "pf_InvestRegisters");

            migrationBuilder.DropColumn(
                name: "unit",
                table: "pf_InvestRegisters");

            migrationBuilder.AddColumn<int>(
                name: "companyid",
                table: "pf_InvestRegisters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "balsheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: true),
                    MDesc = table.Column<string>(nullable: true),
                    acctcode = table.Column<string>(nullable: true),
                    adbbalance = table.Column<decimal>(nullable: true),
                    balSheetCode = table.Column<string>(nullable: true),
                    balpl = table.Column<decimal>(nullable: true),
                    bl_desc = table.Column<string>(nullable: true),
                    crbalance = table.Column<decimal>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    mainAccountCode = table.Column<string>(nullable: true),
                    opbalance = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balsheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "surplus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(type: "money", nullable: true),
                    Expr1 = table.Column<string>(nullable: true),
                    acctcode = table.Column<string>(nullable: true),
                    adbbalance = table.Column<decimal>(type: "money", nullable: true),
                    balSheetCode = table.Column<string>(nullable: true),
                    balpl = table.Column<decimal>(type: "money", nullable: true),
                    bl_desc = table.Column<string>(nullable: true),
                    crbalance = table.Column<decimal>(type: "money", nullable: true),
                    description = table.Column<string>(nullable: true),
                    mainAccountCode = table.Column<string>(nullable: true),
                    opbalance = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surplus", x => x.Id);
                });
        }
    }
}
