using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITEC.Backend.Persistence.Migrations
{
    public partial class desk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LocationX = table.Column<float>(type: "real", nullable: false),
                    LocationY = table.Column<float>(type: "real", nullable: false),
                    IsHotelingDesk = table.Column<bool>(type: "bit", nullable: false),
                    FloorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAtTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desks_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Desks_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desks_CreatedByUserId",
                table: "Desks",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Desks_FloorId",
                table: "Desks",
                column: "FloorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desks");
        }
    }
}
