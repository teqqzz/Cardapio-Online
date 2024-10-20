using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardapioOnline.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarBancoDois : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChamadoGarcom",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChamadoGarcom",
                table: "Pedidos");
        }
    }
}
