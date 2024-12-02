using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardapioOnline.Migrations
{
    /// <inheritdoc />
    public partial class AddItemDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Itens",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Itens",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredientes",
                table: "Itens",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "Ingredientes",
                table: "Itens");
        }
    }
}
