using DevLancheMania.Context;
using DevLancheMania.Models;
using DevLancheMania.Repositories.Interfaces;

namespace DevLancheMania.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DevLancheManiaContext _context;

        public CategoriaRepository(DevLancheManiaContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
