using CardapioOnline.Infra;
using CardapioOnline.Models;
using CardapioOnline.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CardapioOnline.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObterTodas()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> ObterPorId(long id) 
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(long id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Categoria?> ObterPorNome(string nomeCategoria)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == nomeCategoria);
        }

    }
}
