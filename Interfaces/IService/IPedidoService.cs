using CardapioOnline.Models;

namespace CardapioOnline.Interfaces.IService
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ObterTodosPedidos();
        Task<Pedido> ObterPedidoPorId(long id);
        Task<int> RealizarPedido(Pedido pedido);
        Task<decimal?> CalcularSomaTotalPedido(long pedidoId);
        Task<List<Pedido>> ObterPedidosMesaCliente(int numeroMesa);
        Task<int> AtualizarPedido(Pedido pedido);
        Task AdicionarDetalhePedido(Pedido pedido, DetalhePedido detalhePedido);
    }
}
