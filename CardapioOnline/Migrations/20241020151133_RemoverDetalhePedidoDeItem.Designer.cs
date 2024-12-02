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
    [Migration("20241020151133_RemoverDetalhePedidoDeItem")]
    partial class RemoverDetalhePedidoDeItem
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
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
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

            modelBuilder.Entity("CardapioOnline.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("DECIMAL(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Itens");
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
                        .HasColumnType("TEXT");

                    b.Property<int>("NumeroMesaCliente")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("CardapioOnline.Models.DetalhePedido", b =>
                {
                    b.HasOne("CardapioOnline.Models.Item", "Item")
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

            modelBuilder.Entity("CardapioOnline.Models.Item", b =>
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
