using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addNpf_ClaimRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "claimRegisters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    svcno = table.Column<string>(maxLength: 12, nullable: true),
                    appdate = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(maxLength: 12, nullable: true),
                    statusdate = table.Column<DateTime>(nullable: false),
                    TotalContribution = table.Column<decimal>(nullable: false),
                    interest = table.Column<decimal>(nullable: false),
                    loan1 = table.Column<decimal>(nullable: false),
                    loan2 = table.Column<decimal>(nullable: false),
                    loan3 = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(maxLength: 60, nullable: true),
                    AmountDue = table.Column<decimal>(nullable: false),
                    chequeno = table.Column<string>(maxLength: 20, nullable: true),
                    amountPaid = table.Column<decimal>(nullable: false),
                    amountReceived = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claimRegisters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claimRegisters");
        }
    }
}
