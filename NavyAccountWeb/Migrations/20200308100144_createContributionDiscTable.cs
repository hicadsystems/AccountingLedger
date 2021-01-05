using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class createContributionDiscTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "npf_contrdisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fundcode = table.Column<string>(nullable: true),
                    amountpay = table.Column<decimal>(type: "money", nullable: false),
                    prvamt = table.Column<decimal>(type: "money", nullable: false),
                    extamt = table.Column<decimal>(type: "money", nullable: false),
                    amtvar = table.Column<decimal>(type: "money", nullable: false),
                    amtdiscr = table.Column<decimal>(type: "money", nullable: false),
                    contract = table.Column<string>(nullable: true),
                    period = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_npf_contrdisc", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "npf_contrdisc");
        }
    }
}
