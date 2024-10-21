public record DetalhePedidoViewModel
{
    public long Id { get; set; }
    public int Quantidade { get; set; }
    public long PedidoId { get; set; }
    public long ItemId { get; set; }
}
