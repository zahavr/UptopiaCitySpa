using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddedBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.CreateTable(
                name: "Business",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxCountOfWorker = table.Column<int>(type: "int", nullable: false),
                    BusinessStatus = table.Column<int>(type: "int", nullable: false),
                    DateConfirmation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessWorkers",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWork = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PositionAtWork = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessWorkers_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "Business",
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessWorkers_BusinessId",
                schema: "Business",
                table: "BusinessWorkers",
                column: "BusinessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessWorkers",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Business",
                schema: "Business");
        }
    }
}
