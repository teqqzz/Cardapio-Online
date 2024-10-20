using System.Runtime.InteropServices;
using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardapioOnline.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            var categorias = await _categoriaService.ObterTodas();
            var viewModel = categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
            }).ToList();

            return Ok(viewModel);
        }


        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var categoria = await _categoriaService.ObterPorId(id);
            if (categoria == null) return NotFound();

            // Converte a categoria única para a viewmodel
            var viewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Ok(viewModel);
        }

        [HttpGet("{nomeCategoria}")]
        public async Task<IActionResult> ObterItensPorCategoria(string nomeCategoria)
        {
            var itens = await _categoriaService.ObterItensCategoria(nomeCategoria);

            if (itens == null)
            {
                return NotFound("Categoria não encontrada ou sem itens.");
            }

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
        public async Task<IActionResult> Adicionar([FromBody] AdicionarCategoriaViewModel categoria)
        {
            Categoria novaCategoria = new Categoria
            {
                Nome = categoria.Nome
            };
            await _categoriaService.Adicionar(novaCategoria);
            return CreatedAtAction(nameof(ObterPorId), new { id = novaCategoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(long id, [FromBody] AdicionarCategoriaViewModel categoria)
        {
            // Retrieve the existing category from the database
            var categoriaASerAtualizada = await _categoriaService.ObterPorId(id);

            // Check if the category exists
            if (categoriaASerAtualizada == null) return NotFound();

            // Update the existing category's properties
            categoriaASerAtualizada.Nome = categoria.Nome;

            // Now update the existing entity
            await _categoriaService.Atualizar(categoriaASerAtualizada);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            await _categoriaService.Deletar(id);
            return NoContent();
        }
    }
}

