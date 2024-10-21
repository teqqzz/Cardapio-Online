using CardapioOnline.Infra;
using CardapioOnline.Models;
using CardapioOnline.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CardapioOnline.Repository
{
    public class DetalhePedidoRepository : IDetalhePedidoRepository
    {
        private readonly AppDbContext _context;

        public DetalhePedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetalhePedido>> ObterTodos()
        {
            return await _context.DetalhesPedidos.ToListAsync();
        }

        public async Task<DetalhePedido> ObterPorId(long id)
        {
            return await _context.DetalhesPedidos.FindAsync(id);
        }

        public async Task Adicionar(DetalhePedido detalhePedido)
        {
            await _context.DetalhesPedidos.AddAsync(detalhePedido);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(DetalhePedido detalhePedido)
        {
            _context.DetalhesPedidos.Update(detalhePedido);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(long id)
        {
            var detalhe = await _context.DetalhesPedidos.FindAsync(id);
            if (detalhe != null)
            {
                _context.DetalhesPedidos.Remove(detalhe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
