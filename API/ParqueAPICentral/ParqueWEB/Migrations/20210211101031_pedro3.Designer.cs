﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParqueAPICentral.Data;

namespace ParqueAPICentral.Migrations
{
    [DbContext(typeof(APICentralContext))]
    [Migration("20210211101031_pedro3")]
    partial class pedro3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ParqueAPICentral.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Cliente", b =>
                {
                    b.Property<long>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<float>("Credito")
                        .HasColumnType("real");

                    b.Property<string>("EmailCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("MetodoPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NifCliente")
                        .HasColumnType("int");

                    b.Property<string>("NomeCliente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("userId")
                        .HasColumnType("bigint");

                    b.HasKey("ClienteID");

                    b.HasIndex("userId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Fatura", b =>
                {
                    b.Property<long>("FaturaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataFatura")
                        .HasColumnType("datetime2");

                    b.Property<float>("PrecoFatura")
                        .HasColumnType("real");

                    b.Property<long>("ReservaID")
                        .HasColumnType("bigint");

                    b.HasKey("FaturaID");

                    b.HasIndex("ReservaID");

                    b.ToTable("Fatura");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Pagamento", b =>
                {
                    b.Property<long>("PagamentoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("FaturaID")
                        .HasColumnType("bigint");

                    b.HasKey("PagamentoID");

                    b.HasIndex("FaturaID");

                    b.ToTable("Pagamento");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Reserva", b =>
                {
                    b.Property<long>("ReservaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ClienteID")
                        .HasColumnType("bigint");

                    b.Property<long>("NifParqueAPI")
                        .HasColumnType("bigint");

                    b.Property<long>("ReservaAPI")
                        .HasColumnType("bigint");

                    b.HasKey("ReservaID");

                    b.HasIndex("ClienteID");

                    b.ToTable("Reserva");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.SubAluguer", b =>
                {
                    b.Property<long>("SubAluguerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataSubAluguer")
                        .HasColumnType("datetime2");

                    b.Property<float>("PrecoSubAluguer")
                        .HasColumnType("real");

                    b.Property<long>("ReservaID")
                        .HasColumnType("bigint");

                    b.HasKey("SubAluguerID");

                    b.HasIndex("ReservaID");

                    b.ToTable("SubAluguer");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Cliente", b =>
                {
                    b.HasOne("ParqueAPICentral.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Fatura", b =>
                {
                    b.HasOne("ParqueAPICentral.Models.Reserva", "Reserva")
                        .WithMany()
                        .HasForeignKey("ReservaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Pagamento", b =>
                {
                    b.HasOne("ParqueAPICentral.Models.Fatura", "Fatura")
                        .WithMany()
                        .HasForeignKey("FaturaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fatura");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.Reserva", b =>
                {
                    b.HasOne("ParqueAPICentral.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ParqueAPICentral.Models.SubAluguer", b =>
                {
                    b.HasOne("ParqueAPICentral.Models.Reserva", "reserva")
                        .WithMany()
                        .HasForeignKey("ReservaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("reserva");
                });
#pragma warning restore 612, 618
        }
    }
}
