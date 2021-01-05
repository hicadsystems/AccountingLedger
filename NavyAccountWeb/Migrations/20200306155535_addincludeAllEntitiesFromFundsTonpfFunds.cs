using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addincludeAllEntitiesFromFundsTonpfFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fundname",
                table: "npffundType",
                newName: "trusteeacct");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createdby",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "datecreated",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fundacct",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "incomeacct",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "interestacct",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "jvno",
                table: "npffundType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "processingMonth",
                table: "npffundType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "processingYear",
                table: "npffundType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "profitacct",
                table: "npffundType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "receiptno",
                table: "npffundType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "startDate",
                table: "npffundType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "createdby",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "datecreated",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "fundacct",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "incomeacct",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "interestacct",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "jvno",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "processingMonth",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "processingYear",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "profitacct",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "receiptno",
                table: "npffundType");

            migrationBuilder.DropColumn(
                name: "startDate",
                table: "npffundType");

            migrationBuilder.RenameColumn(
                name: "trusteeacct",
                table: "npffundType",
                newName: "fundname");
        }
    }
}
