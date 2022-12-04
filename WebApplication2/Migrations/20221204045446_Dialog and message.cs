using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class Dialogandmessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Parents_idParent",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Shifts_idShift",
                table: "Request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Request");

            migrationBuilder.RenameTable(
                name: "Request",
                newName: "Requests");

            migrationBuilder.RenameColumn(
                name: "idParent",
                table: "Requests",
                newName: "idChild");

            migrationBuilder.RenameIndex(
                name: "IX_Request_idShift",
                table: "Requests",
                newName: "IX_Requests_idShift");

            migrationBuilder.RenameIndex(
                name: "IX_Request_idParent",
                table: "Requests",
                newName: "IX_Requests_idChild");

            migrationBuilder.AddColumn<int>(
                name: "ParentidParent",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "idRequest");

            migrationBuilder.CreateTable(
                name: "Dialogs",
                columns: table => new
                {
                    idDialog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idParent = table.Column<int>(type: "int", nullable: false),
                    idOrganization = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogs", x => x.idDialog);
                    table.ForeignKey(
                        name: "FK_Dialogs_Organizations_idOrganization",
                        column: x => x.idOrganization,
                        principalTable: "Organizations",
                        principalColumn: "IdOrganization",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dialogs_Parents_idParent",
                        column: x => x.idParent,
                        principalTable: "Parents",
                        principalColumn: "idParent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    idMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    textMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idDialog = table.Column<int>(type: "int", nullable: false),
                    messageTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idParent = table.Column<int>(type: "int", nullable: true),
                    idOrganization = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.idMessage);
                    table.ForeignKey(
                        name: "FK_Messages_Dialogs_idDialog",
                        column: x => x.idDialog,
                        principalTable: "Dialogs",
                        principalColumn: "idDialog",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Organizations_idOrganization",
                        column: x => x.idOrganization,
                        principalTable: "Organizations",
                        principalColumn: "IdOrganization");
                    table.ForeignKey(
                        name: "FK_Messages_Parents_idParent",
                        column: x => x.idParent,
                        principalTable: "Parents",
                        principalColumn: "idParent");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ParentidParent",
                table: "Requests",
                column: "ParentidParent");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_idOrganization",
                table: "Dialogs",
                column: "idOrganization");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_idParent",
                table: "Dialogs",
                column: "idParent");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_idDialog",
                table: "Messages",
                column: "idDialog");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_idOrganization",
                table: "Messages",
                column: "idOrganization");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_idParent",
                table: "Messages",
                column: "idParent");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Children_idChild",
                table: "Requests",
                column: "idChild",
                principalTable: "Children",
                principalColumn: "idChild",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Parents_ParentidParent",
                table: "Requests",
                column: "ParentidParent",
                principalTable: "Parents",
                principalColumn: "idParent");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Shifts_idShift",
                table: "Requests",
                column: "idShift",
                principalTable: "Shifts",
                principalColumn: "idShift",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Children_idChild",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Parents_ParentidParent",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Shifts_idShift",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Dialogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ParentidParent",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ParentidParent",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameColumn(
                name: "idChild",
                table: "Request",
                newName: "idParent");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_idShift",
                table: "Request",
                newName: "IX_Request_idShift");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_idChild",
                table: "Request",
                newName: "IX_Request_idParent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Request",
                column: "idRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Parents_idParent",
                table: "Request",
                column: "idParent",
                principalTable: "Parents",
                principalColumn: "idParent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Shifts_idShift",
                table: "Request",
                column: "idShift",
                principalTable: "Shifts",
                principalColumn: "idShift",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
