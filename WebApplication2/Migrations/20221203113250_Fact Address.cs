using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class FactAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idFactAddress",
                table: "Parents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idFactAddress",
                table: "Children",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FactAddresses",
                columns: table => new
                {
                    idFactAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    index = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCitizenship = table.Column<int>(type: "int", nullable: true),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    houseHumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    housing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactAddresses", x => x.idFactAddress);
                    table.ForeignKey(
                        name: "FK_FactAddresses_Citizenships_idCitizenship",
                        column: x => x.idCitizenship,
                        principalTable: "Citizenships",
                        principalColumn: "idCitizenship");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    idRequest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idParent = table.Column<int>(type: "int", nullable: false),
                    AmountToBePaid = table.Column<double>(type: "float", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    idShift = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.idRequest);
                    table.ForeignKey(
                        name: "FK_Request_Parents_idParent",
                        column: x => x.idParent,
                        principalTable: "Parents",
                        principalColumn: "idParent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Shifts_idShift",
                        column: x => x.idShift,
                        principalTable: "Shifts",
                        principalColumn: "idShift",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_idFactAddress",
                table: "Parents",
                column: "idFactAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Children_idFactAddress",
                table: "Children",
                column: "idFactAddress");

            migrationBuilder.CreateIndex(
                name: "IX_FactAddresses_idCitizenship",
                table: "FactAddresses",
                column: "idCitizenship");

            migrationBuilder.CreateIndex(
                name: "IX_Request_idParent",
                table: "Request",
                column: "idParent");

            migrationBuilder.CreateIndex(
                name: "IX_Request_idShift",
                table: "Request",
                column: "idShift");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_FactAddresses_idFactAddress",
                table: "Children",
                column: "idFactAddress",
                principalTable: "FactAddresses",
                principalColumn: "idFactAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_FactAddresses_idFactAddress",
                table: "Parents",
                column: "idFactAddress",
                principalTable: "FactAddresses",
                principalColumn: "idFactAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_FactAddresses_idFactAddress",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_FactAddresses_idFactAddress",
                table: "Parents");

            migrationBuilder.DropTable(
                name: "FactAddresses");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Parents_idFactAddress",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Children_idFactAddress",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "idFactAddress",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "idFactAddress",
                table: "Children");
        }
    }
}
