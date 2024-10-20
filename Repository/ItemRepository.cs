using CardapioOnline.Infra;
using CardapioOnline.Interfaces.IRepository;
using CardapioOnline.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardapioOnline.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _contexto;

        public ItemRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Item>> ObterTodos()
        {
            return await _contexto.Itens.ToListAsync();
        }

        public async Task<Item> ObterPorId(long id)
        {
            return await _contexto.Itens.FindAsync(id);
        }

        public async Task Adicionar(Item item)
        {
            await _contexto.Itens.AddAsync(item);
            await _contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Item item)
        {
            _contexto.Itens.Update(item);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar(long id)
        {
            var item = await _contexto.Itens.FindAsync(id);
            if (item != null)
            {
                _contexto.Itens.Remove(item);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
