using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class schoolupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "sr_StudentRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClassCategory",
                table: "sr_StudentRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentalStatus",
                table: "sr_StudentRecord",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolCount",
                table: "sr_SchoolRecord",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "sr_ClassRecord",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassName = table.Column<string>(nullable: true),
                    SchoolType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_ClassRecord", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_ClassRecord");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "ClassCategory",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "ParentalStatus",
                table: "sr_StudentRecord");

            migrationBuilder.DropColumn(
                name: "SchoolCount",
                table: "sr_SchoolRecord");
        }
    }
}
