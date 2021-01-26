﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParqueAPI.Migrations
{
    public partial class ss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NifCliente = table.Column<long>(type: "bigint", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credito = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteID);
                });

            migrationBuilder.CreateTable(
                name: "Morada",
                columns: table => new
                {
                    MoradaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Morada", x => x.MoradaID);
                });

            migrationBuilder.CreateTable(
                name: "Parque",
                columns: table => new
                {
                    ParqueID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeParque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParquePublico = table.Column<bool>(type: "bit", nullable: false),
                    MoradaID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parque", x => x.ParqueID);
                    table.ForeignKey(
                        name: "FK_Parque_Morada_MoradaID",
                        column: x => x.MoradaID,
                        principalTable: "Morada",
                        principalColumn: "MoradaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lugar",
                columns: table => new
                {
                    LugarID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fila = table.Column<int>(type: "int", nullable: false),
                    Sector = table.Column<int>(type: "int", nullable: false),
                    Preço = table.Column<float>(type: "real", nullable: false),
                    ParqueID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lugar", x => x.LugarID);
                    table.ForeignKey(
                        name: "FK_Lugar_Parque_ParqueID",
                        column: x => x.ParqueID,
                        principalTable: "Parque",
                        principalColumn: "ParqueID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPagamentoReserva = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrePagamento = table.Column<bool>(type: "bit", nullable: false),
                    ClienteID = table.Column<long>(type: "bigint", nullable: false),
                    LugarID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaID);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Lugar_LugarID",
                        column: x => x.LugarID,
                        principalTable: "Lugar",
                        principalColumn: "LugarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    FaturaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataFatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPagamentoFatura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecoFatura = table.Column<float>(type: "real", nullable: false),
                    ReservaID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.FaturaID);
                    table.ForeignKey(
                        name: "FK_Fatura_Reserva_ReservaID",
                        column: x => x.ReservaID,
                        principalTable: "Reserva",
                        principalColumn: "ReservaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubAluguer",
                columns: table => new
                {
                    SubAluguerID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservaID = table.Column<long>(type: "bigint", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false),
                    Preço = table.Column<float>(type: "real", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteID1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAluguer", x => x.SubAluguerID);
                    table.ForeignKey(
                        name: "FK_SubAluguer_Cliente_ClienteID1",
                        column: x => x.ClienteID1,
                        principalTable: "Cliente",
                        principalColumn: "ClienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubAluguer_Reserva_ReservaID",
                        column: x => x.ReservaID,
                        principalTable: "Reserva",
                        principalColumn: "ReservaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_ReservaID",
                table: "Fatura",
                column: "ReservaID");

            migrationBuilder.CreateIndex(
                name: "IX_Lugar_ParqueID",
                table: "Lugar",
                column: "ParqueID");

            migrationBuilder.CreateIndex(
                name: "IX_Parque_MoradaID",
                table: "Parque",
                column: "MoradaID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ClienteID",
                table: "Reserva",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_LugarID",
                table: "Reserva",
                column: "LugarID");

            migrationBuilder.CreateIndex(
                name: "IX_SubAluguer_ClienteID1",
                table: "SubAluguer",
                column: "ClienteID1");

            migrationBuilder.CreateIndex(
                name: "IX_SubAluguer_ReservaID",
                table: "SubAluguer",
                column: "ReservaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DropTable(
                name: "SubAluguer");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Lugar");

            migrationBuilder.DropTable(
                name: "Parque");

            migrationBuilder.DropTable(
                name: "Morada");
        }
    }
}