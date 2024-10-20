using CardapioOnline.Models;

namespace CardapioOnline.Interfaces.IRepository
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObterTodas();
        Task<Categoria?> ObterPorId(long id);
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Deletar(long id);

        Task<Categoria> ObterPorNome(string nomeCategoria);

    }
}
