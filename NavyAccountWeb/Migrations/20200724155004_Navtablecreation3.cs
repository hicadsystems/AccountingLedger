using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Navtablecreation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "npf_NavipContribution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "reviewDate",
                table: "npf_Contributions",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "reviewDate2",
                table: "npf_Contributions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "reviewDate2",
                table: "npf_Contributions");

            migrationBuilder.AlterColumn<string>(
                name: "reviewDate",
                table: "npf_Contributions",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
