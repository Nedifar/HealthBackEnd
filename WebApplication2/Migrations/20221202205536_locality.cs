using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class locality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizenships",
                columns: table => new
                {
                    idCitizenship = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizenships", x => x.idCitizenship);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    IdOrganization = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.IdOrganization);
                });

            migrationBuilder.CreateTable(
                name: "Pasports",
                columns: table => new
                {
                    idPersonalDocument = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    series = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    issuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacementDocument = table.Column<int>(type: "int", nullable: false),
                    TypePersonalDocument = table.Column<int>(type: "int", nullable: false),
                    validityTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasports", x => x.idPersonalDocument);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    idRegion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    regionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.idRegion);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationAddresses",
                columns: table => new
                {
                    idRegistrationAddress = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    index = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCitizenship = table.Column<int>(type: "int", nullable: false),
                    region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    houseHumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    housing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationAddresses", x => x.idRegistrationAddress);
                    table.ForeignKey(
                        name: "FK_RegistrationAddresses_Citizenships_idCitizenship",
                        column: x => x.idCitizenship,
                        principalTable: "Citizenships",
                        principalColumn: "idCitizenship",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Camps",
                columns: table => new
                {
                    idCamp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    campName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    campType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SesionalShift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    supportTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    housingCount = table.Column<int>(type: "int", nullable: false),
                    territoryArea = table.Column<double>(type: "float", nullable: false),
                    foodInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    haveSportObjects = table.Column<bool>(type: "bit", nullable: false),
                    TermsAndPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdOrganization = table.Column<int>(type: "int", nullable: false),
                    OrganizationIdOrganization = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camps", x => x.idCamp);
                    table.ForeignKey(
                        name: "FK_Camps_Organizations_OrganizationIdOrganization",
                        column: x => x.OrganizationIdOrganization,
                        principalTable: "Organizations",
                        principalColumn: "IdOrganization");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    idDistrict = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idRegion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.idDistrict);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_idRegion",
                        column: x => x.idRegion,
                        principalTable: "Regions",
                        principalColumn: "idRegion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    idParent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentStatus = table.Column<int>(type: "int", nullable: false),
                    idCitizenship = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    idPersonalDocument = table.Column<int>(type: "int", nullable: true),
                    idRegistrationAddress = table.Column<int>(type: "int", nullable: true),
                    Snils = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailIsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    confirmCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.idParent);
                    table.ForeignKey(
                        name: "FK_Parents_Citizenships_idCitizenship",
                        column: x => x.idCitizenship,
                        principalTable: "Citizenships",
                        principalColumn: "idCitizenship");
                    table.ForeignKey(
                        name: "FK_Parents_Pasports_idPersonalDocument",
                        column: x => x.idPersonalDocument,
                        principalTable: "Pasports",
                        principalColumn: "idPersonalDocument");
                    table.ForeignKey(
                        name: "FK_Parents_RegistrationAddresses_idRegistrationAddress",
                        column: x => x.idRegistrationAddress,
                        principalTable: "RegistrationAddresses",
                        principalColumn: "idRegistrationAddress");
                });

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    idLocality = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idDistrict = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.idLocality);
                    table.ForeignKey(
                        name: "FK_Localities_Districts_idDistrict",
                        column: x => x.idDistrict,
                        principalTable: "Districts",
                        principalColumn: "idDistrict",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    idChild = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCitizenship = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idPersonalDocument = table.Column<int>(type: "int", nullable: false),
                    idRegistrationAddress = table.Column<int>(type: "int", nullable: false),
                    Snils = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idParent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.idChild);
                    table.ForeignKey(
                        name: "FK_Children_Citizenships_idCitizenship",
                        column: x => x.idCitizenship,
                        principalTable: "Citizenships",
                        principalColumn: "idCitizenship",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Children_Parents_idParent",
                        column: x => x.idParent,
                        principalTable: "Parents",
                        principalColumn: "idParent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Children_Pasports_idPersonalDocument",
                        column: x => x.idPersonalDocument,
                        principalTable: "Pasports",
                        principalColumn: "idPersonalDocument",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Children_RegistrationAddresses_idRegistrationAddress",
                        column: x => x.idRegistrationAddress,
                        principalTable: "RegistrationAddresses",
                        principalColumn: "idRegistrationAddress",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    idStreet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idLocality = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.idStreet);
                    table.ForeignKey(
                        name: "FK_Streets_Localities_idLocality",
                        column: x => x.idLocality,
                        principalTable: "Localities",
                        principalColumn: "idLocality",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camps_OrganizationIdOrganization",
                table: "Camps",
                column: "OrganizationIdOrganization");

            migrationBuilder.CreateIndex(
                name: "IX_Children_idCitizenship",
                table: "Children",
                column: "idCitizenship");

            migrationBuilder.CreateIndex(
                name: "IX_Children_idParent",
                table: "Children",
                column: "idParent");

            migrationBuilder.CreateIndex(
                name: "IX_Children_idPersonalDocument",
                table: "Children",
                column: "idPersonalDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Children_idRegistrationAddress",
                table: "Children",
                column: "idRegistrationAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_idRegion",
                table: "Districts",
                column: "idRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_idDistrict",
                table: "Localities",
                column: "idDistrict");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_idCitizenship",
                table: "Parents",
                column: "idCitizenship");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_idPersonalDocument",
                table: "Parents",
                column: "idPersonalDocument");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_idRegistrationAddress",
                table: "Parents",
                column: "idRegistrationAddress");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationAddresses_idCitizenship",
                table: "RegistrationAddresses",
                column: "idCitizenship");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_idLocality",
                table: "Streets",
                column: "idLocality");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camps");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.DropTable(
                name: "Pasports");

            migrationBuilder.DropTable(
                name: "RegistrationAddresses");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Citizenships");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
