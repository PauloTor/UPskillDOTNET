using Microsoft.EntityFrameworkCore.Migrations;

namespace ParquePrivateAPI.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetodoPagamentoReserva",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "PrePagamento",
                table: "Reserva");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetodoPagamentoReserva",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrePagamento",
                table: "Reserva",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
