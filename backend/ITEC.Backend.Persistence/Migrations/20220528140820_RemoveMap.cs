using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITEC.Backend.Persistence.Migrations
{
    public partial class RemoveMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_UploadedFiles_MapId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Floors_MapId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Floors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "Floors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Floors_MapId",
                table: "Floors",
                column: "MapId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_UploadedFiles_MapId",
                table: "Floors",
                column: "MapId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
