using Microsoft.EntityFrameworkCore;
using futshop_dweb.Models;
using DW_Final_Project.Models;

namespace futshop_dweb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Artigos> Artigos { get; set; }
        public DbSet<Carrinho> carrinho { get; set; }
        public DbSet<CarrinhoArtigo> CarrinhoArtigo { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        public bool IsUserLoggedIn()
        {
            return Global.LoggedUser!=null;
        }

    }
}
