using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParqueAPI.Migrations
{
    public partial class llll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "SubAluguer");

            migrationBuilder.DropColumn(
                name: "ParquePublico",
                table: "Parque");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "SubAluguer",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "TipoParque",
                table: "Parque",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoParque",
                table: "Parque");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "SubAluguer",
                newName: "DataInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "SubAluguer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ParquePublico",
                table: "Parque",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
