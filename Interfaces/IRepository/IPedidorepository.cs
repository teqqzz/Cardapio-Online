using CardapioOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardapioOnline.Interfaces.IRepository
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> ObterTodos();
        Task<Pedido> ObterPorId(long id);
        Task Adicionar(Pedido pedido);
        Task Atualizar(Pedido pedido);
        Task Deletar(long id);
    }
}
