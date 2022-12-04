using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class inn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BIK",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckCorres",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckNumber",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPP",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OGRN",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OKPO",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BIK",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CheckCorres",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CheckNumber",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "INN",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "KPP",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OGRN",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OKPO",
                table: "Organizations");
        }
    }
}
