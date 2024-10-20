namespace CardapioOnline.Models;

using System.ComponentModel.DataAnnotations;

public class Categoria
{
    [Key]
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public DateTime? AtualizadoEm { get; set; }
    public List<Item> Itens { get; set; } = new List<Item>();
}

