using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardapioOnline.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeCliente",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPedido",
                table: "Pedidos",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Itens",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Categorias",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Categorias",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Categorias");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCliente",
                table: "Pedidos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataPedido",
                table: "Pedidos",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Itens",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
