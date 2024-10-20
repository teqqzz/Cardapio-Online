public record ItemViewModel
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public long CategoriaId { get; set; }
}

