using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Core.Migrations
{
    public partial class ExtendedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introducation",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KnownAs",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ApplicationUserId",
                schema: "Identity",
                table: "Photos",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos",
                schema: "Identity");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Interests",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Introducation",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "KnownAs",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastActive",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                schema: "Identity",
                table: "User");
        }
    }
}
