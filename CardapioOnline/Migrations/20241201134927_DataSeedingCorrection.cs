using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardapioOnline.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CriadoEm",
                value: new DateTime(2024, 12, 1, 10, 49, 26, 853, DateTimeKind.Local).AddTicks(8748));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CriadoEm",
                value: new DateTime(2024, 12, 1, 10, 49, 26, 853, DateTimeKind.Local).AddTicks(8783));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Preco",
                value: 15.99m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CriadoEm",
                value: new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CriadoEm",
                value: new DateTime(2024, 12, 1, 9, 46, 48, 79, DateTimeKind.Local).AddTicks(4407));

            migrationBuilder.UpdateData(
                table: "Itens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Preco",
                value: 0m);
        }
    }
}
