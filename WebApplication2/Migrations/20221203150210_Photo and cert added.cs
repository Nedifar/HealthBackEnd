using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class Photoandcertadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CampPhotos",
                columns: table => new
                {
                    idCampPhoto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCamp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampPhotos", x => x.idCampPhoto);
                    table.ForeignKey(
                        name: "FK_CampPhotos_Camps_idCamp",
                        column: x => x.idCamp,
                        principalTable: "Camps",
                        principalColumn: "idCamp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificate",
                columns: table => new
                {
                    idCertificate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCamp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.idCertificate);
                    table.ForeignKey(
                        name: "FK_Certificate_Camps_idCamp",
                        column: x => x.idCamp,
                        principalTable: "Camps",
                        principalColumn: "idCamp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    idFeedback = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idParent = table.Column<int>(type: "int", nullable: false),
                    datePublished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estimation = table.Column<int>(type: "int", nullable: false),
                    idShift = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.idFeedback);
                    table.ForeignKey(
                        name: "FK_Feedback_Parents_idParent",
                        column: x => x.idParent,
                        principalTable: "Parents",
                        principalColumn: "idParent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_Shifts_idShift",
                        column: x => x.idShift,
                        principalTable: "Shifts",
                        principalColumn: "idShift",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampPhotos_idCamp",
                table: "CampPhotos",
                column: "idCamp");

            migrationBuilder.CreateIndex(
                name: "IX_Certificate_idCamp",
                table: "Certificate",
                column: "idCamp");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_idParent",
                table: "Feedback",
                column: "idParent");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_idShift",
                table: "Feedback",
                column: "idShift");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampPhotos");

            migrationBuilder.DropTable(
                name: "Certificate");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Request");
        }
    }
}
