using CardapioOnline.Infra;
using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;
using Microsoft.EntityFrameworkCore;

public class ItemService : IItemService
{
    private readonly AppDbContext _contexto;
    public ItemService(AppDbContext contexto)
    {
        _contexto = contexto;
    }
    public async Task<Item?> ObterItemPorId(long itemId)=>
        await _contexto.Itens.FirstOrDefaultAsync(i => i.Id == itemId);

    public async Task<List<Item>> ObterTodosItens() =>
        await _contexto.Itens.ToListAsync();

    public async Task<int> AdicionarItem(Item item)
    {
        await _contexto.Itens.AddAsync(item);
        return await _contexto.SaveChangesAsync();
    }
}