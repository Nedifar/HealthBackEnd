using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class mxm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Pasports_idPersonalDocument",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_RegistrationAddresses_idRegistrationAddress",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "idRegistrationAddress",
                table: "Children",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idPersonalDocument",
                table: "Children",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Pasports_idPersonalDocument",
                table: "Children",
                column: "idPersonalDocument",
                principalTable: "Pasports",
                principalColumn: "idPersonalDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_RegistrationAddresses_idRegistrationAddress",
                table: "Children",
                column: "idRegistrationAddress",
                principalTable: "RegistrationAddresses",
                principalColumn: "idRegistrationAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Pasports_idPersonalDocument",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_RegistrationAddresses_idRegistrationAddress",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "idRegistrationAddress",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idPersonalDocument",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Pasports_idPersonalDocument",
                table: "Children",
                column: "idPersonalDocument",
                principalTable: "Pasports",
                principalColumn: "idPersonalDocument",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_RegistrationAddresses_idRegistrationAddress",
                table: "Children",
                column: "idRegistrationAddress",
                principalTable: "RegistrationAddresses",
                principalColumn: "idRegistrationAddress",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
