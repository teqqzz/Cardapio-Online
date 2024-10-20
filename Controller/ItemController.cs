using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardapioOnline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosItens()
        {
            var itens = await _itemService.ObterTodosItens();
            var viewModel = itens.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Nome = i.Nome,
                Preco = i.Preco,
                CategoriaId = i.CategoriaId
            }).ToList();

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItem(AdicionarItemViewModel item)
        {
            Item novoItem = new Item{
                CategoriaId = item.CategoriaId,
                Nome = item.Nome,
                Preco = item.Preco
            };
            
            await _itemService.AdicionarItem(novoItem);
            return CreatedAtAction(nameof(ObterTodosItens), new { id = novoItem.Id }, item);
        }
    }
}
