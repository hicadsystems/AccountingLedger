using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addsr_defaulter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Parentid",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Guardianid",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitDate",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommencementDate",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "sr_StudentRecordHist",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "sr_Defaulter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    Term = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_Defaulter", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_Defaulter");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Parentid",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Guardianid",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitDate",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommencementDate",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "sr_StudentRecordHist",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
