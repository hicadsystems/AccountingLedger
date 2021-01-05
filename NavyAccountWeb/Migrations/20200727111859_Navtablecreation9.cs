using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Navtablecreation9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "acctno",
                table: "npf_NavipContribution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bank",
                table: "npf_NavipContribution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "npf_NavipContribution",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "acctno",
                table: "npf_navip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bank",
                table: "npf_navip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "npf_navip",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "title",
                table: "npf_navip",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "acctno",
                table: "Npf_ClaimRegisters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "bank",
                table: "Npf_ClaimRegisters",
                nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "bank2",
            //    table: "Npf_ClaimRegisters",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acctno",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "bank",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "remark",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "acctno",
                table: "npf_navip");

            migrationBuilder.DropColumn(
                name: "bank",
                table: "npf_navip");

            migrationBuilder.DropColumn(
                name: "remark",
                table: "npf_navip");

            migrationBuilder.DropColumn(
                name: "title",
                table: "npf_navip");

            migrationBuilder.DropColumn(
                name: "acctno",
                table: "Npf_ClaimRegisters");

            migrationBuilder.DropColumn(
                name: "bank",
                table: "Npf_ClaimRegisters");

            //migrationBuilder.DropColumn(
            //    name: "bank2",
            //    table: "Npf_ClaimRegisters");
        }
    }
}
