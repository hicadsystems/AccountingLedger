using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updatedBeneficiary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_relationships_RelationshipId",
                table: "Beneficiaries");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_RelationshipId",
                table: "Beneficiaries");

            migrationBuilder.AlterColumn<string>(
                name: "RelationshipId",
                table: "Beneficiaries",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelationshipId1",
                table: "Beneficiaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_RelationshipId1",
                table: "Beneficiaries",
                column: "RelationshipId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_relationships_RelationshipId1",
                table: "Beneficiaries",
                column: "RelationshipId1",
                principalTable: "relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_relationships_RelationshipId1",
                table: "Beneficiaries");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_RelationshipId1",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "RelationshipId1",
                table: "Beneficiaries");

            migrationBuilder.AlterColumn<int>(
                name: "RelationshipId",
                table: "Beneficiaries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_RelationshipId",
                table: "Beneficiaries",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_relationships_RelationshipId",
                table: "Beneficiaries",
                column: "RelationshipId",
                principalTable: "relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
