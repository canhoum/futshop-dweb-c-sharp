using Microsoft.EntityFrameworkCore;
using futshop_dweb.Models;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Identity;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Utilizador>().HasData(
            new Utilizador { UtilizadorId = 1, Nome = "Sistema", Telemovel = "919999999", DataNascimento = DateOnly.FromDateTime(DateTime.Now), Email = "sistema@gmail.com", morada ="Sistema", codigopostal = "4000-000", Cidade = "Sistema", Pais = "Portugal", Password = "Admin123", IsAdmin = true }
        );
    }

        public bool IsUserLoggedIn()
        {
            return Global.LoggedUser!=null;
        }
           }
        }

    