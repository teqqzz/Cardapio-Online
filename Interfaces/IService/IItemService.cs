using CardapioOnline.Models;

namespace CardapioOnline.Interfaces.IService
{
    public interface IItemService
    {
        Task<List<Item>> ObterTodosItens();
        Task<Item?> ObterItemPorId(long itemId);
        Task<int> AdicionarItem(Item item);
    }
}

