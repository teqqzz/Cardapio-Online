using CardapioOnline.Interfaces.IService;
using CardapioOnline.Infra;
using CardapioOnline.Models;
using Microsoft.EntityFrameworkCore;

public class PedidoService : IPedidoService
{
    private readonly AppDbContext _contexto;

    public PedidoService(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Pedido>> ObterTodosPedidos() =>
        await _contexto.Pedidos.ToListAsync();

    public async Task<int> RealizarPedido(Pedido pedido)
    {
        await _contexto.Pedidos.AddAsync(pedido);
        return await _contexto.SaveChangesAsync();
    }

    public async Task<List<Pedido>> ObterPedidosMesaCliente(int numeroMesa) =>
        await _contexto.Pedidos.Where(o => o.NumeroMesaCliente == numeroMesa).ToListAsync();

    public async Task<Pedido> ObterPedidoPorId(long id) =>
        await _contexto.Pedidos.FindAsync(id);

    public async Task<int> AtualizarPedido(Pedido pedido)
    {
        _contexto.Pedidos.Update(pedido);
        return await _contexto.SaveChangesAsync();
    }

    public async Task<decimal?> CalcularSomaTotalPedido(long pedidoId)
    {
        var pedido = await _contexto.Pedidos
            .Include(p => p.DetalhesPedido)
            .ThenInclude(dp => dp.Item)
            .FirstOrDefaultAsync(p => p.Id == pedidoId);

        if (pedido == null)return null;

        decimal total = pedido.DetalhesPedido.Sum(dp => dp.Quantidade * dp.Item.Preco);
        return total;
    }
    public async Task AdicionarDetalhePedido(Pedido pedido, DetalhePedido detalhePedido)
    {
        // Adicionar o detalhe ao pedido
        pedido.DetalhesPedido.Add(detalhePedido);
        // Salvar as mudanças no banco de dados
        await _contexto.SaveChangesAsync();
    }
}
