using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class CreatedLoanPerRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loantype",
                table: "npf_Contributions");

            migrationBuilder.CreateTable(
                name: "loanPerRank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    loantype = table.Column<string>(nullable: true),
                    amount01 = table.Column<decimal>(nullable: false),
                    amount02 = table.Column<decimal>(nullable: false),
                    amount03 = table.Column<decimal>(nullable: false),
                    amount04 = table.Column<decimal>(nullable: false),
                    amount05 = table.Column<decimal>(nullable: false),
                    amount06 = table.Column<decimal>(nullable: false),
                    amount07 = table.Column<decimal>(nullable: false),
                    amount08 = table.Column<decimal>(nullable: false),
                    amount09 = table.Column<decimal>(nullable: false),
                    amount10 = table.Column<decimal>(nullable: false),
                    amount11 = table.Column<decimal>(nullable: false),
                    amount12 = table.Column<decimal>(nullable: false),
                    amount13 = table.Column<decimal>(nullable: false),
                    amount14 = table.Column<decimal>(nullable: false),
                    amount15 = table.Column<decimal>(nullable: false),
                    amount16 = table.Column<decimal>(nullable: false),
                    amount17 = table.Column<decimal>(nullable: false),
                    amount18 = table.Column<decimal>(nullable: false),
                    amount19 = table.Column<decimal>(nullable: false),
                    amount20 = table.Column<decimal>(nullable: false),
                    amount21 = table.Column<decimal>(nullable: false),
                    amount22 = table.Column<decimal>(nullable: false),
                    Createdby = table.Column<string>(nullable: true),
                    Datecreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loanPerRank", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loanPerRank");

            migrationBuilder.AddColumn<string>(
                name: "loantype",
                table: "npf_Contributions",
                nullable: true);
        }
    }
}
