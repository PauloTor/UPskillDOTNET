using Microsoft.EntityFrameworkCore.Migrations;

namespace ParqueAPI.Migrations
{
    public partial class sss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_Cliente_ClienteID",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_ClienteID",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Reserva");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClienteID",
                table: "Reserva",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteID",
                table: "Reserva",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_Cliente_ClienteID",
                table: "Reserva",
                column: "ClienteID",
                principalTable: "Cliente",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
