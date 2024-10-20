public record PedidoViewModel
{
    public long Id { get; set; }
    public string NomeCliente { get; set; }
    public DateTime DataPedido { get; set; }
    public int NumeroMesaCliente { get; set; }
}
