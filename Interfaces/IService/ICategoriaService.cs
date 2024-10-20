using CardapioOnline.Models;

namespace CardapioOnline.Interfaces.IService
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObterTodas();
        Task<Categoria> ObterPorId(long id);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Deletar(long id);

        Task<List<ItemViewModel>> ObterItensCategoria(string nomeCategoria);
    }
}


