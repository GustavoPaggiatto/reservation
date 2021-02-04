using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ContactType",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contactTypeId = table.Column<int>(type: "int", nullable: false),
                    logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.id);
                    table.ForeignKey(
                        name: "FK_Contact_ContactType_contactTypeId",
                        column: x => x.contactTypeId,
                        principalSchema: "dbo",
                        principalTable: "ContactType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserve",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    ranking = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    favorite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserve", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserve_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "dbo",
                        principalTable: "Contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ContactType",
                columns: new[] { "id", "description" },
                values: new object[] { 1, "Phisical" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ContactType",
                columns: new[] { "id", "description" },
                values: new object[] { 2, "Company" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_contactTypeId",
                schema: "dbo",
                table: "Contact",
                column: "contactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserve_ContactId",
                schema: "dbo",
                table: "Reserve",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserve",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ContactType",
                schema: "dbo");
        }
    }
}
