using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Navtablecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "npf_NavipContribution",
                columns: table => new
                {
                    svcno = table.Column<string>(maxLength: 30, nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    prmdate01 = table.Column<DateTime>(nullable: true),
                    prmdate02 = table.Column<DateTime>(nullable: true),
                    prmdate03 = table.Column<DateTime>(nullable: true),
                    prmdate04 = table.Column<DateTime>(nullable: true),
                    prmdate05 = table.Column<DateTime>(nullable: true),
                    prmdate06 = table.Column<DateTime>(nullable: true),
                    prmdate07 = table.Column<DateTime>(nullable: true),
                    prmdate08 = table.Column<DateTime>(nullable: true),
                    prmdate09 = table.Column<DateTime>(nullable: true),
                    prmdate10 = table.Column<DateTime>(nullable: true),
                    prmdate11 = table.Column<DateTime>(nullable: true),
                    prmdate12 = table.Column<DateTime>(nullable: true),
                    prmdate13 = table.Column<DateTime>(nullable: true),
                    prmdate14 = table.Column<DateTime>(nullable: true),
                    prmdate15 = table.Column<DateTime>(nullable: true),
                    prmdate16 = table.Column<DateTime>(nullable: true),
                    prmdate17 = table.Column<DateTime>(nullable: true),
                    prmdate18 = table.Column<DateTime>(nullable: true),
                    prmdate19 = table.Column<DateTime>(nullable: true),
                    prmdate20 = table.Column<DateTime>(nullable: true),
                    prmdate21 = table.Column<DateTime>(nullable: true),
                    prmdate22 = table.Column<DateTime>(nullable: true),
                    datecreated = table.Column<DateTime>(nullable: true),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_NavipContribution", x => x.svcno);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "npf_NavipContribution");
        }
    }
}
