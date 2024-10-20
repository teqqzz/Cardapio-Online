namespace CardapioOnline.Models;

using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public long Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public long CategoriaId { get; set; }

    public Categoria Categoria { get; set; }
}
