using DevLancheMania.Context;
using DevLancheMania.Models;
using Microsoft.EntityFrameworkCore;

namespace DevLancheMania.Areas.Admin.Services
{
    public class RelatorioLanchesService
    {
        private readonly DevLancheManiaContext _context;

        public RelatorioLanchesService(DevLancheManiaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lanche>> GetLanchesReport()
        {
            var lanches = await _context.Lanches.ToListAsync();

            if(lanches is null)
                return default(IEnumerable<Lanche>);

            return lanches;          
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasReport()
        {
            var categorias = await _context.Categorias.ToListAsync();

            if (categorias is null)
                return default(IEnumerable<Categoria>);

            return categorias;
        }
    }
}
