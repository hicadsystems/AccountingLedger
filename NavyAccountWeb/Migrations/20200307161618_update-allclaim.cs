using Microsoft.EntityFrameworkCore.Migrations;

namespace NavyAccountWeb.Migrations
{
    public partial class updateallclaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_claimRegisters",
                table: "claimRegisters");

            migrationBuilder.RenameTable(
                name: "claimRegisters",
                newName: "Npf_ClaimRegisters");

            migrationBuilder.AlterColumn<decimal>(
                name: "loan3",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "loan2",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "loan1",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "interest",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "amountReceived",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "amountPaid",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalContribution",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDue",
                table: "Npf_ClaimRegisters",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Npf_ClaimRegisters",
                table: "Npf_ClaimRegisters",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Npf_ClaimRegisters",
                table: "Npf_ClaimRegisters");

            migrationBuilder.RenameTable(
                name: "Npf_ClaimRegisters",
                newName: "claimRegisters");

            migrationBuilder.AlterColumn<decimal>(
                name: "loan3",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "loan2",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "loan1",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "interest",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "amountReceived",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "amountPaid",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalContribution",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountDue",
                table: "claimRegisters",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_claimRegisters",
                table: "claimRegisters",
                column: "Id");
        }
    }
}
