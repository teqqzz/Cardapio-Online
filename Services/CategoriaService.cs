using CardapioOnline.Interfaces.IRepository;
using CardapioOnline.Interfaces.IService;
using CardapioOnline.Models;

namespace CardapioOnline.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Categoria>> ObterTodas()
        {
            return await _categoriaRepository.ObterTodas();
        }

        public async Task<Categoria> ObterPorId(long id)
        {
            return await _categoriaRepository.ObterPorId(id);
        }

        public async Task Adicionar(Categoria categoria)
        {
            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Deletar(long id)
        {
            await _categoriaRepository.Deletar(id);
        }

        public async Task<List<ItemViewModel>> ObterItensCategoria(string nomeCategoria)
        {
            var categoria = await _categoriaRepository.ObterPorNome(nomeCategoria);
            if (categoria == null)
            {
                return new List<ItemViewModel>();
            }

            return categoria.Itens.Select(i => new ItemViewModel
            {
                Id = i.Id,
                Nome = i.Nome,
                Preco = i.Preco,
                CategoriaId = i.CategoriaId
            }).ToList();
        }


    }
}
