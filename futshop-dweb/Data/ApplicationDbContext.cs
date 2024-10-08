﻿using Microsoft.EntityFrameworkCore;
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
            new Categoria { Id = 1, Nome = "Liga Betclic" },
            new Categoria { Id = 2, Nome = "Premier League" },
            new Categoria { Id = 3, Nome = "La Liga" },
            new Categoria { Id = 4, Nome = "Liga Betclic Retro" },
            new Categoria { Id = 5, Nome = "Premier League Retro" },
            new Categoria { Id = 6, Nome = "La Liga Retro" },
            new Categoria { Id = 7, Nome = "Europa" },
            new Categoria { Id = 8, Nome = "América" },
            new Categoria { Id = 9, Nome = "África" }

        );

         modelBuilder.Entity<Artigos>().HasData(
             new Artigos { Id = 1, Nome = "Camisola Benfica" , Descricao = "Camisola do Benfica Oficial" , Tamanho = "M" , Quantidade= 10 , Preco=20 , ImagemURL = "" , CategoriaFK = 1},
             new Artigos { Id = 2, Nome = "Camisola Manchester City", Descricao = "Camisola do Manchester City Oficial", Tamanho = "L", Quantidade = 20, Preco = 22, ImagemURL = "", CategoriaFK = 2 },
             new Artigos { Id = 3, Nome = "Camisola Real Madrid", Descricao = "Camisola do Real Madrid Oficial", Tamanho = "L", Quantidade = 50, Preco = 18, ImagemURL = "", CategoriaFK = 3 },
             new Artigos { Id = 4, Nome = "Camisola Benfica Retro", Descricao = "Camisola do Benfica Retro Oficial", Tamanho = "S", Quantidade = 19, Preco = 50, ImagemURL = "", CategoriaFK = 4 }
             );
    }

        public bool IsUserLoggedIn()
        {
            return Global.LoggedUser!=null;
        }
           }
        }

    