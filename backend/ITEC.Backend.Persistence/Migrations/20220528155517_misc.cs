using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITEC.Backend.Persistence.Migrations
{
    public partial class misc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "Desks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Order",
                table: "Desks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
