using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class BuildingPictureUrlAndFixCountAppartaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessWorkers_Business_BusinessId",
                schema: "Business",
                table: "BusinessWorkers");

            migrationBuilder.RenameColumn(
                name: "CountApartments",
                schema: "HousingSystem",
                table: "Buildings",
                newName: "CountAppartments");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                schema: "Business",
                table: "BusinessWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                schema: "HousingSystem",
                table: "Appartaments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessWorkers_Business_BusinessId",
                schema: "Business",
                table: "BusinessWorkers",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessWorkers_Business_BusinessId",
                schema: "Business",
                table: "BusinessWorkers");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                schema: "HousingSystem",
                table: "Appartaments");

            migrationBuilder.RenameColumn(
                name: "CountAppartments",
                schema: "HousingSystem",
                table: "Buildings",
                newName: "CountApartments");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                schema: "Business",
                table: "BusinessWorkers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessWorkers_Business_BusinessId",
                schema: "Business",
                table: "BusinessWorkers",
                column: "BusinessId",
                principalSchema: "Business",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
