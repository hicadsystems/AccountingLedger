using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class addschoolControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "term",
                table: "sr_SchoolFeeTB",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sr_SchoolRecordControl",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<string>(nullable: true),
                    term = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SchoolCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sr_SchoolRecordControl", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sr_SchoolRecordControl");

            migrationBuilder.DropColumn(
                name: "term",
                table: "sr_SchoolFeeTB");
        }
    }
}
