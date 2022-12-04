using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class Campandshiftupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SesionalShift",
                table: "Camps");

            migrationBuilder.DropColumn(
                name: "campType",
                table: "Camps");

            migrationBuilder.AddColumn<int>(
                name: "TypeCamp",
                table: "Camps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    idShift = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeasonCamp = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    idCamp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.idShift);
                    table.ForeignKey(
                        name: "FK_Shifts_Camps_idCamp",
                        column: x => x.idCamp,
                        principalTable: "Camps",
                        principalColumn: "idCamp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_idCamp",
                table: "Shifts",
                column: "idCamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropColumn(
                name: "TypeCamp",
                table: "Camps");

            migrationBuilder.AddColumn<string>(
                name: "SesionalShift",
                table: "Camps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "campType",
                table: "Camps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
