using CardapioOnline.Models;

namespace CardapioOnline.Interfaces.IService
{
    public interface IDetalhePedidoService
    {
        Task<List<DetalhePedido>> ObterTodosDetalhesPedidos();
        Task<DetalhePedido> ObterDetalhePedidoPorId(long id);
        Task<int> AdicionarDetalhePedido(DetalhePedido detalhePedido);
        Task<int> AtualizarDetalhePedido(DetalhePedido detalhePedido);
        Task<int> DeletarDetalhePedido(long id);
        Task<decimal?> CalcularSomaTotalPedido(long pedidoId);
    }
}

