using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class idCitizenship1nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Citizenships_idCitizenship",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "idCitizenship",
                table: "Children",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Citizenships_idCitizenship",
                table: "Children",
                column: "idCitizenship",
                principalTable: "Citizenships",
                principalColumn: "idCitizenship");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Citizenships_idCitizenship",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "idCitizenship",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Citizenships_idCitizenship",
                table: "Children",
                column: "idCitizenship",
                principalTable: "Citizenships",
                principalColumn: "idCitizenship",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
