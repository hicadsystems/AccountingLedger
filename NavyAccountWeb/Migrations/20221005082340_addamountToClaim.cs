using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addamountToClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitDate",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommencementDate",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClaimAmount",
                table: "sr_StudentRecord",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ClaimDate",
                table: "sr_StudentRecord",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Schoolfee",
                table: "sr_ClassRecord",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimAmount",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "ClaimDate",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "Schoolfee",
                table: "sr_ClassRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ExitDate",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CommencementDate",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
