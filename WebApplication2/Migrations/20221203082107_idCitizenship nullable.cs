using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class idCitizenshipnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationAddresses_Citizenships_idCitizenship",
                table: "RegistrationAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "idCitizenship",
                table: "RegistrationAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationAddresses_Citizenships_idCitizenship",
                table: "RegistrationAddresses",
                column: "idCitizenship",
                principalTable: "Citizenships",
                principalColumn: "idCitizenship");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationAddresses_Citizenships_idCitizenship",
                table: "RegistrationAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "idCitizenship",
                table: "RegistrationAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationAddresses_Citizenships_idCitizenship",
                table: "RegistrationAddresses",
                column: "idCitizenship",
                principalTable: "Citizenships",
                principalColumn: "idCitizenship",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
