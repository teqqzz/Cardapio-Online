using CardapioOnline.Infra;
using CardapioOnline.Models;
using Microsoft.EntityFrameworkCore;
using CardapioOnline.Interfaces.IService;

namespace CardapioOnline.Services
{
    public class DetalhePedidoService : IDetalhePedidoService
    {
        private readonly AppDbContext _contexto;

        public DetalhePedidoService(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<DetalhePedido>> ObterTodosDetalhesPedidos() =>
            await _contexto.DetalhesPedidos.ToListAsync();

        public async Task<DetalhePedido> ObterDetalhePedidoPorId(long id) =>
            await _contexto.DetalhesPedidos.FindAsync(id);

        public async Task<int> AdicionarDetalhePedido(DetalhePedido detalhePedido)
        {
            await _contexto.DetalhesPedidos.AddAsync(detalhePedido);
            return await _contexto.SaveChangesAsync();
        }

        public async Task<int> AtualizarDetalhePedido(DetalhePedido detalhePedido)
        {
            _contexto.DetalhesPedidos.Update(detalhePedido);
            return await _contexto.SaveChangesAsync();
        }

        public async Task<int> DeletarDetalhePedido(long id)
        {
            var detalhe = await _contexto.DetalhesPedidos.FindAsync(id);
            if (detalhe == null)
                return 0;

            _contexto.DetalhesPedidos.Remove(detalhe);
            return await _contexto.SaveChangesAsync();
        }

        public async Task<decimal?> CalcularSomaTotalPedido(long pedidoId)
        {
            Console.WriteLine("----------------------ID DO PEDIDO É: ---------------------------");
            Console.WriteLine(pedidoId);
            var pedido = await _contexto.Pedidos
                .Include(p => p.DetalhesPedido)
                .ThenInclude(dp => dp.Item)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                return null;

            decimal total = pedido.DetalhesPedido
                .Sum(dp => dp.Quantidade * dp.Item.Preco);

            return total;
        }
    } 
} 
