using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class RebuildBuildings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResidentId",
                table: "Appartaments");

            migrationBuilder.EnsureSchema(
                name: "HousingSystem");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "Buildings",
                newSchema: "HousingSystem");

            migrationBuilder.RenameTable(
                name: "Appartaments",
                newName: "Appartaments",
                newSchema: "HousingSystem");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                schema: "HousingSystem",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypeAppartament",
                schema: "HousingSystem",
                table: "Appartaments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "UserAppartament",
                schema: "HousingSystem",
                columns: table => new
                {
                    AppartamentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAppartament", x => x.AppartamentId);
                    table.ForeignKey(
                        name: "FK_UserAppartament_Appartaments_AppartamentId",
                        column: x => x.AppartamentId,
                        principalSchema: "HousingSystem",
                        principalTable: "Appartaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAppartament",
                schema: "HousingSystem");

            migrationBuilder.RenameTable(
                name: "Buildings",
                schema: "HousingSystem",
                newName: "Buildings");

            migrationBuilder.RenameTable(
                name: "Appartaments",
                schema: "HousingSystem",
                newName: "Appartaments");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TypeAppartament",
                table: "Appartaments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ResidentId",
                table: "Appartaments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
