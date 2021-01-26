﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParqueAPI.Data;

namespace ParqueAPI.Migrations
{
    [DbContext(typeof(ParqueAPIContext))]
    [Migration("20210126123427_InitialMigrateParqueAPI")]
    partial class InitialMigrateParqueAPI
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ParqueAPI.Models.Cliente", b =>
                {
                    b.Property<long>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<float>("Credito")
                        .HasColumnType("real");

                    b.Property<string>("EmailCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetodoPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NifCliente")
                        .HasColumnType("bigint");

                    b.Property<string>("NomeCliente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteID");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ParqueAPI.Models.Fatura", b =>
                {
                    b.Property<long>("FaturaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataFatura")
                        .HasColumnType("datetime2");

                    b.Property<string>("MetodoPagamentoFatura")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PrecoFatura")
                        .HasColumnType("real");

                    b.Property<long>("ReservaID")
                        .HasColumnType("bigint");

                    b.HasKey("FaturaID");

                    b.HasIndex("ReservaID");

                    b.ToTable("Fatura");
                });

            modelBuilder.Entity("ParqueAPI.Models.Lugar", b =>
                {
                    b.Property<long>("LugarID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("Fila")
                        .HasColumnType("int");

                    b.Property<long>("ParqueID")
                        .HasColumnType("bigint");

                    b.Property<float>("Preço")
                        .HasColumnType("real");

                    b.Property<int>("Sector")
                        .HasColumnType("int");

                    b.HasKey("LugarID");

                    b.HasIndex("ParqueID");

                    b.ToTable("Lugar");
                });

            modelBuilder.Entity("ParqueAPI.Models.Morada", b =>
                {
                    b.Property<long>("MoradaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MoradaID");

                    b.ToTable("Morada");
                });

            modelBuilder.Entity("ParqueAPI.Models.Parque", b =>
                {
                    b.Property<long>("ParqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("MoradaID")
                        .HasColumnType("bigint");

                    b.Property<string>("NomeParque")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoParque")
                        .HasColumnType("int");

                    b.HasKey("ParqueID");

                    b.HasIndex("MoradaID");

                    b.ToTable("Parque");
                });

            modelBuilder.Entity("ParqueAPI.Models.Reserva", b =>
                {
                    b.Property<long>("ReservaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ClienteID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataReserva")
                        .HasColumnType("datetime2");

                    b.Property<long>("LugarID")
                        .HasColumnType("bigint");

                    b.Property<string>("MetodoPagamentoReserva")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PrePagamento")
                        .HasColumnType("bit");

                    b.HasKey("ReservaID");

                    b.HasIndex("ClienteID");

                    b.HasIndex("LugarID");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("ParqueAPI.Models.SubAluguer", b =>
                {
                    b.Property<long>("SubAluguerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<long?>("ClienteID1")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<float>("Preço")
                        .HasColumnType("real");

                    b.Property<long>("ReservaID")
                        .HasColumnType("bigint");

                    b.HasKey("SubAluguerID");

                    b.HasIndex("ClienteID1");

                    b.HasIndex("ReservaID");

                    b.ToTable("SubAluguer");
                });

            modelBuilder.Entity("ParqueAPI.Models.Fatura", b =>
                {
                    b.HasOne("ParqueAPI.Models.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("ParqueAPI.Models.Lugar", b =>
                {
                    b.HasOne("ParqueAPI.Models.Parque", "parque")
                        .WithMany()
                        .HasForeignKey("ParqueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("parque");
                });

            modelBuilder.Entity("ParqueAPI.Models.Parque", b =>
                {
                    b.HasOne("ParqueAPI.Models.Morada", "Morada")
                        .WithMany()
                        .HasForeignKey("MoradaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Morada");
                });

            modelBuilder.Entity("ParqueAPI.Models.Reserva", b =>
                {
                    b.HasOne("ParqueAPI.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ParqueAPI.Models.Lugar", "Lugar")
                        .WithMany()
                        .HasForeignKey("LugarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Lugar");
                });

            modelBuilder.Entity("ParqueAPI.Models.SubAluguer", b =>
                {
                    b.HasOne("ParqueAPI.Models.Cliente", "cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID1");

                    b.HasOne("ParqueAPI.Models.Reserva", "reserva")
                        .WithMany()
                        .HasForeignKey("ReservaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");

                    b.Navigation("reserva");
                });
#pragma warning restore 612, 618
        }
    }
}