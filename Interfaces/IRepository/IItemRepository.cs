using CardapioOnline.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardapioOnline.Interfaces.IRepository
{
    public interface IItemRepository
    {
        Task<List<Item>> ObterTodos();
        Task<Item> ObterPorId(long id);
        Task Adicionar(Item item);
        Task Atualizar(Item item);
        Task Deletar(long id);
    }
}
