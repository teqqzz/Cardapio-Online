using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;
using Microsoft.AspNetCore.Mvc;
using CardapioOnline.Services;

namespace CardapioOnline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalhePedidoController : ControllerBase
    {
        private readonly IDetalhePedidoService _detalhePedidoService;

        public DetalhePedidoController(IDetalhePedidoService detalhePedidoService)
        {
            _detalhePedidoService = detalhePedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosDetalhes()
        {
            var detalhes = await _detalhePedidoService.ObterTodosDetalhesPedidos();
            var viewModel = detalhes.Select(d => new DetalhePedidoViewModel
            {
                Id = d.Id,
                Quantidade = d.Quantidade,
                PedidoId = d.PedidoId,
                ItemId = d.ItemId,
            }).ToList();

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var detalhe = await _detalhePedidoService.ObterDetalhePedidoPorId(id);
            if (detalhe == null) return NotFound();
            return Ok(detalhe);
        }
    }

}
