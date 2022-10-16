using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addStudentHist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Parentid",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Guardianid",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitDate",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommencementDate",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClaimDate",
                table: "sr_StudentRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<decimal>(
                name: "ClaimAmount",
                table: "sr_StudentRecord",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.CreateTable(
                name: "sr_StudentRecordHist",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Parentid = table.Column<int>(nullable: false),
                    Guardianid = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    ClassCategory = table.Column<string>(nullable: true),
                    ParentalStatus = table.Column<string>(nullable: true),
                    SchoolCode = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    CommencementDate = table.Column<DateTime>(nullable: false),
                    Class = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: false),
                    ExitReason = table.Column<string>(nullable: true),
                    ClaimAmount = table.Column<decimal>(nullable: false),
                    ClaimDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_StudentRecordHist", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_StudentRecordHist");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolId",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Parentid",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Guardianid",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitDate",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommencementDate",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ClaimDate",
                table: "sr_StudentRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ClaimAmount",
                table: "sr_StudentRecord",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);
        }
    }
}
