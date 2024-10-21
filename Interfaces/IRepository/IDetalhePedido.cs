using CardapioOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardapioOnline.Interfaces.IRepository
{
    public interface IDetalhePedidoRepository
    {
        Task<List<DetalhePedido>> ObterTodos();
        Task<DetalhePedido> ObterPorId(long id);
        Task Adicionar(DetalhePedido detalhePedido);
        Task Atualizar(DetalhePedido detalhePedido);
        Task Deletar(long id);
    }
}
