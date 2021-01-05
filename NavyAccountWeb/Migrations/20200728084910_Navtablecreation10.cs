using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class Navtablecreation10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.AddColumn<string>(
                name: "beneficiary",
                table: "npf_NavipContribution",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "title",
                table: "npf_NavipContribution",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "beneficiary",
                table: "npf_NavipContribution");

            migrationBuilder.DropColumn(
                name: "title",
                table: "npf_NavipContribution");


        }
    }
}
