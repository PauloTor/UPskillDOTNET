using Microsoft.EntityFrameworkCore.Migrations;

namespace ParqueAPICentral.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NovoCliente",
                table: "SubAluguer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Reservado",
                table: "SubAluguer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NovoCliente",
                table: "SubAluguer");

            migrationBuilder.DropColumn(
                name: "Reservado",
                table: "SubAluguer");
        }
    }
}
