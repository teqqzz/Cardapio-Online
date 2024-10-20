using CardapioOnline.Infra;
using CardapioOnline.Interfaces.IRepository;
using CardapioOnline.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardapioOnline.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _contexto;

        public PedidoRepository(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Pedido>> ObterTodos()
        {
            return await _contexto.Pedidos.ToListAsync();
        }

        public async Task<Pedido> ObterPorId(long id)
        {
            return await _contexto.Pedidos.FindAsync(id);
        }

        public async Task Adicionar(Pedido pedido)
        {
            await _contexto.Pedidos.AddAsync(pedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task Atualizar(Pedido pedido)
        {
            _contexto.Pedidos.Update(pedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task Deletar(long id)
        {
            var pedido = await _contexto.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _contexto.Pedidos.Remove(pedido);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
