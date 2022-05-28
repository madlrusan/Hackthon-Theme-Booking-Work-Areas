using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITEC.Backend.Persistence.Migrations
{
    public partial class reservations_and_desk_stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsHotelingDesk",
                table: "Desks",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "DeskReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeskId = table.Column<int>(type: "int", nullable: false),
                    CreatedAtTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeskReservation_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeskReservation_Desks_DeskId",
                        column: x => x.DeskId,
                        principalTable: "Desks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeskReservation_CreatedByUserId",
                table: "DeskReservation",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskReservation_DeskId",
                table: "DeskReservation",
                column: "DeskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskReservation");

            migrationBuilder.AlterColumn<bool>(
                name: "IsHotelingDesk",
                table: "Desks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }
    }
}
