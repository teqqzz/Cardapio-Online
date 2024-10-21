namespace CardapioOnline.Models;

using System.ComponentModel.DataAnnotations;

public class DetalhePedido
{
    [Key]
    public long Id { get; set; }
    public int Quantidade { get; set; }

    public long PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public long ItemId { get; set; }
    public Item Item { get; set; }
}
