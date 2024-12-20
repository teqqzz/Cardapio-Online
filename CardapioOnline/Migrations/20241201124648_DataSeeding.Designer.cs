﻿// <auto-generated />
using System;
using CardapioOnline.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardapioOnline.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241201124648_DataSeeding")]
    partial class DataSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("CardapioOnline.Models.Categoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CriadoEm = new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4363),
                            Nome = "Bebidas"
                        },
                        new
                        {
                            Id = 2L,
                            CriadoEm = new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4407),
                            Nome = "Entradas"
                        });
                });

            modelBuilder.Entity("CardapioOnline.Models.DetalhePedido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PedidoId");

                    b.ToTable("DetalhesPedidos");
                });

            modelBuilder.Entity("CardapioOnline.Models.Pedido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ChamadoGarcom")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("DATETIME");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("NumeroMesaCliente")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagemUrl")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ingredientes")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("DECIMAL(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Itens");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CategoriaId = 1L,
                            Nome = "Coca-Cola",
                            Preco = 6.99m
                        },
                        new
                        {
                            Id = 2L,
                            CategoriaId = 2L,
                            Nome = "Empanado",
                            Preco = 0m
                        });
                });

            modelBuilder.Entity("CardapioOnline.Models.DetalhePedido", b =>
                {
                    b.HasOne("Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardapioOnline.Models.Pedido", "Pedido")
                        .WithMany("DetalhesPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.HasOne("CardapioOnline.Models.Categoria", "Categoria")
                        .WithMany("Itens")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("CardapioOnline.Models.Categoria", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("CardapioOnline.Models.Pedido", b =>
                {
                    b.Navigation("DetalhesPedido");
                });
#pragma warning restore 612, 618
        }
    }
}
