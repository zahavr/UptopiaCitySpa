using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Data.Migrations
{
	public partial class Friend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Friends");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDateUser = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FriendId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FriendEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "Friends");
        }
    }
}
