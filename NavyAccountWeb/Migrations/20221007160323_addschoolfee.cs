using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addschoolfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "sr_StudentRecord",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Transdate",
                table: "sr_ClaimRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "sr_ClaimRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "sr_ClaimRecord",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "sr_ClaimRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sr_SchoolFeeTB",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<string>(nullable: true),
                    ClassCategory = table.Column<string>(nullable: true),
                    ClassId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_SchoolFeeTB", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_SchoolFeeTB");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "sr_ClaimRecord");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Transdate",
                table: "sr_ClaimRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedDate",
                table: "sr_ClaimRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "sr_ClaimRecord",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
