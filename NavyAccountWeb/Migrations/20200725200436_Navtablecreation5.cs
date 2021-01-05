using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Navtablecreation5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_npf_NavipContribution",
            //    table: "npf_NavipContribution");

            //migrationBuilder.AlterColumn<string>(
            //    name: "svcno",
            //    table: "npf_NavipContribution",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Batch",
                table: "npf_NavipContribution",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_npf_NavipContribution",
                table: "npf_NavipContribution",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "npf_navip",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Batch = table.Column<string>(maxLength: 30, nullable: true),
                    svcno = table.Column<string>(maxLength: 30, nullable: true),
                    rank = table.Column<int>(nullable: false),
                    reviewdate1 = table.Column<DateTime>(type: "date", nullable: false),
                    reviewdate2 = table.Column<DateTime>(type: "date", nullable: false),
                    surval = table.Column<decimal>(type: "money", nullable: false),
                    smonth = table.Column<int>(nullable: false),
                    calcdate1 = table.Column<DateTime>(type: "date", nullable: false),
                    caldate2 = table.Column<DateTime>(nullable: false),
                    datecreated = table.Column<DateTime>(nullable: false),
                    createdby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_npf_navip", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "npf_navip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_npf_NavipContribution",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "Batch",
                table: "npf_NavipContribution");

            migrationBuilder.AlterColumn<string>(
                name: "svcno",
                table: "npf_NavipContribution",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_npf_NavipContribution",
                table: "npf_NavipContribution",
                column: "svcno");
        }
    }
}
