using DevLancheMania.Context;
using DevLancheMania.Models;
using DevLancheMania.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevLancheMania.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly DevLancheManiaContext _context;

        public LancheRepository(DevLancheManiaContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c=> c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
            .Where(p=> p.IsLanchePreferido).Include(c=> c.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(p => p.LancheId == lancheId);
        }
    }
}
