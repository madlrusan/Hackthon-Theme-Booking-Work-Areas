using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITEC.Backend.Persistence.Migrations
{
    public partial class RemoveDeskCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationX",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "LocationY",
                table: "Desks");

            migrationBuilder.AddColumn<long>(
                name: "Order",
                table: "Desks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Desks");

            migrationBuilder.AddColumn<float>(
                name: "LocationX",
                table: "Desks",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LocationY",
                table: "Desks",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
