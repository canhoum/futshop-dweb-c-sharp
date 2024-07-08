using DW_Final_Project.Models;
using futshop_dweb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace futshop_dweb.Data
{
    public class ApplicationDbContext : IdentityDbContext
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
    }
}
