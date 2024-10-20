namespace CardapioOnline.Models;

using System.ComponentModel.DataAnnotations;

public class Pedido
{
    public long Id { get; set; }
    public string NomeCliente { get; set; }
    public DateTime DataPedido { get; set; }
    public int NumeroMesaCliente { get; set; }
    public bool ChamadoGarcom { get; set; } = false;
    public List<DetalhePedido> DetalhesPedido { get; set; } = new List<DetalhePedido>();
}


