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

        public DbSet<Transacao_Artigo> Transacao_Artigo { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Utilizador>().HasData(
            new Utilizador { UtilizadorId = 1, Nome = "Sistema", Telemovel = "919999999", DataNascimento = DateOnly.FromDateTime(DateTime.Now), Email = "sistema@gmail.com", morada ="Sistema", codigopostal = "4000-000", Cidade = "Sistema", Pais = "Portugal", Password = "Admin123", IsAdmin = true },
            new Utilizador { UtilizadorId = 2, Nome = "a", Telemovel = "919999999", DataNascimento = DateOnly.FromDateTime(DateTime.Now), Email = "a@a.com", morada = "a", codigopostal = "4000-000", Cidade = "a", Pais = "Portugal", Password = "12345", IsAdmin = false }
        );

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Liga Portuguesa" },
            new Categoria { Id = 2, Nome = "Liga Espanhola" }
        );
    }

        public bool IsUserLoggedIn()
        {
            return Global.LoggedUser!=null;
        }
           }
        }

    