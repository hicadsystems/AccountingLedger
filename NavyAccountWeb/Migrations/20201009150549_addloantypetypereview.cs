using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addloantypetypereview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "batchno",
                table: "pf_loandisc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "batchNo",
                table: "npf_Histories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "npf_loantypereview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoanType = table.Column<string>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    Interestrate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_loantypereview", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "npf_loantypereview");

            migrationBuilder.DropColumn(
                name: "batchno",
                table: "pf_loandisc");

            migrationBuilder.DropColumn(
                name: "batchNo",
                table: "npf_Histories");
        }
    }
}
