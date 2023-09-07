using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updateschoolfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "sr_SchoolFeeTB");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "sr_SchoolFeeTB");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "sr_SchoolFeeTB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentStatus",
                table: "sr_SchoolFeeTB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "sr_SchoolFeeTB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "sr_SchoolFeeTB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "sr_SchoolFeeTB");

            migrationBuilder.DropColumn(
                name: "ParentStatus",
                table: "sr_SchoolFeeTB");

            migrationBuilder.DropColumn(
                name: "School",
                table: "sr_SchoolFeeTB");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "sr_SchoolFeeTB");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "sr_SchoolFeeTB",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "sr_SchoolFeeTB",
                nullable: false,
                defaultValue: 0);
        }
    }
}
