using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CardapioOnline.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Nome" },
                values: new object[,]
                {
                    { 1L, null, new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4363), "Bebidas" },
                    { 2L, null, new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4407), "Entradas" }
                });

            migrationBuilder.InsertData(
                table: "Itens",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImagemUrl", "Ingredientes", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1L, 1L, null, null, null, "Coca-Cola", 6.99m },
                    { 2L, 2L, null, null, null, "Empanado", 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
