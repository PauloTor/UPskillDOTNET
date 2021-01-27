﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParquePrivateAPI.Data;

namespace ParquePrivateAPI.Migrations
{
    [DbContext(typeof(ParquePrivateAPIContext))]
    [Migration("20210127150627_initial create")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParquePrivateAPI.Models.Lugar", b =>
                {
                    b.Property<long>("LugarID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("ParquePrivateAPI.Models.Morada", b =>
                {
                    b.Property<long>("MoradaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoPostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MoradaID");

                    b.ToTable("Morada");
                });

            modelBuilder.Entity("ParquePrivateAPI.Models.Parque", b =>
                {
                    b.Property<long>("ParqueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Lotacao")
                        .HasColumnType("int");

                    b.Property<long>("MoradaID")
                        .HasColumnType("bigint");

                    b.Property<string>("NomeParque")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParqueID");

                    b.HasIndex("MoradaID");

                    b.ToTable("Parque");
                });

            modelBuilder.Entity("ParquePrivateAPI.Models.Reserva", b =>
                {
                    b.Property<long>("ReservaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasIndex("LugarID");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("ParquePrivateAPI.Models.Lugar", b =>
                {
                    b.HasOne("ParquePrivateAPI.Models.Parque", "parque")
                        .WithMany()
                        .HasForeignKey("ParqueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParquePrivateAPI.Models.Parque", b =>
                {
                    b.HasOne("ParquePrivateAPI.Models.Morada", "Morada")
                        .WithMany()
                        .HasForeignKey("MoradaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ParquePrivateAPI.Models.Reserva", b =>
                {
                    b.HasOne("ParquePrivateAPI.Models.Lugar", "Lugar")
                        .WithMany()
                        .HasForeignKey("LugarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
