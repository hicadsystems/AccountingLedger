using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class studentrecordtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "batchno",
            //    table: "npf_loanstatus");

            //migrationBuilder.DropColumn(
            //    name: "personid",
            //    table: "npf_loanstatus");

            migrationBuilder.CreateTable(
                name: "sr_ClaimRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    VoucherNumber = table.Column<string>(nullable: true),
                    Transdate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_ClaimRecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sr_GuardianRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    OtherNames = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Workclass = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusReason = table.Column<string>(nullable: true),
                    StatusDate = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_GuardianRecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sr_ParentRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    OtherNames = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Workclass = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusReason = table.Column<string>(nullable: true),
                    StatusDate = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_ParentRecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sr_PaymentRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Period = table.Column<string>(nullable: true),
                    Transdate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_PaymentRecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sr_SchoolRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolCode = table.Column<string>(nullable: true),
                    Schoolname = table.Column<string>(nullable: true),
                    SchoolAddress = table.Column<string>(nullable: true),
                    SchoolCity = table.Column<string>(nullable: true),
                    SchoolLGA = table.Column<string>(nullable: true),
                    SchoolState = table.Column<string>(nullable: true),
                    SchoolType = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_SchoolRecord", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sr_StudentRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reg_Number = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    SchoolCode = table.Column<string>(nullable: true),
                    CommencementDate = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ExitDate = table.Column<string>(nullable: true),
                    ExitReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_StudentRecord", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_ClaimRecord");

            migrationBuilder.DropTable(
                name: "sr_GuardianRecord");

            migrationBuilder.DropTable(
                name: "sr_ParentRecord");

            migrationBuilder.DropTable(
                name: "sr_PaymentRecord");

            migrationBuilder.DropTable(
                name: "sr_SchoolRecord");

            migrationBuilder.DropTable(
                name: "sr_StudentRecord");

            //migrationBuilder.AddColumn<string>(
            //    name: "batchno",
            //    table: "npf_loanstatus",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "personid",
            //    table: "npf_loanstatus",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
