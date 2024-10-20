using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;
using CardapioOnline.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardapioOnline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IDetalhePedidoService _detalhePedidoService;
        private readonly IItemService _itemService;

        public PedidoController(IPedidoService pedidoService, IDetalhePedidoService detalhePedidoService, 
        IItemService itemService)
        {
            _pedidoService = pedidoService;
            _detalhePedidoService = detalhePedidoService;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosPedidos()
        {
            var pedidos = await _pedidoService.ObterTodosPedidos();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> RealizarPedido(AdicionarPedidoViewModel pedido)
        {
            Pedido novoPedido = new Pedido{
                DataPedido = pedido.DataPedido,
                NomeCliente = pedido.NomeCliente,
                NumeroMesaCliente = pedido.NumeroMesaCliente
            };
            await _pedidoService.RealizarPedido(novoPedido);
            return CreatedAtAction(nameof(ObterTodosPedidos), new { id = novoPedido.Id }, pedido);
        }

        [HttpGet("mesa/{numeroMesa}")]
        public async Task<IActionResult> ObterPedidosMesaCliente(int numeroMesa)
        {
            var pedidos = await _pedidoService.ObterPedidosMesaCliente(numeroMesa);
            return Ok(pedidos);
        }

        [HttpGet("{id}/total")]
        public async Task<IActionResult> CalcularSomaTotalPedido(long id)
        {
            decimal? total = await _pedidoService.CalcularSomaTotalPedido(id);
            if(total == null) return NotFound("Pedido não encontrado");
            return Ok(total);
        }

        [HttpPut("{id}/chamar-garcom")]
        public async Task<IActionResult> ChamarGarcom(long id)
        {
            var pedido = await _pedidoService.ObterPedidoPorId(id);
            if (pedido == null)
                return NotFound();

            pedido.ChamadoGarcom = true;
            await _pedidoService.AtualizarPedido(pedido);

            return Ok("Garçom foi chamado.");
        }

        [HttpPost("adicionar-detalhe")]
        public async Task<IActionResult> AdicionarDetalhePedido([FromBody] AdicionarDetalhePedidoViewModel detalhePedido)
        {
            // Verificar se o pedido existe
            var pedido = await _pedidoService.ObterPedidoPorId(detalhePedido.PedidoId);
            if (pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }
            
            // Verificar se o item existe
            var item = await _itemService.ObterItemPorId(detalhePedido.ItemId);
            if (item == null)
            {
                return NotFound("Item não encontrado.");
            }

            // Associar o pedido e o item ao detalhe
            DetalhePedido novoDetalhePedido = new DetalhePedido{
                Quantidade = detalhePedido.Quantidade,
                PedidoId = pedido.Id,
                ItemId = item.Id
            };

            await _pedidoService.AdicionarDetalhePedido(pedido, novoDetalhePedido);
            
            return Ok(pedido);
        }
    }
}
