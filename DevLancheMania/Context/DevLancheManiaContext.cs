using DevLancheMania.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevLancheMania.Context
{
    public class DevLancheManiaContext : IdentityDbContext<IdentityUser>
    {
        public DevLancheManiaContext(DbContextOptions<DevLancheManiaContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public DbSet<Pedido>  Pedidos { get; set; }

        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}
