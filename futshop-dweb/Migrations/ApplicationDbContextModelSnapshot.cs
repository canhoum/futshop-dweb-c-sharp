﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using futshop_dweb.Data;

#nullable disable

namespace futshop_dweb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DW_Final_Project.Models.Artigos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaFK")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Tamanho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaFK");

                    b.ToTable("Artigos");
                });

            modelBuilder.Entity("futshop_dweb.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Liga Portuguesa"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Liga Espanhola"
                        });
                });

            modelBuilder.Entity("futshop_dweb.Models.Transacao", b =>
                {
                    b.Property<int>("CompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompraId"));

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UtilizadorFK")
                        .HasColumnType("int");

                    b.HasKey("CompraId");

                    b.HasIndex("UtilizadorFK");

                    b.ToTable("Transacao");
                });

            modelBuilder.Entity("futshop_dweb.Models.Transacao_Artigo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtigoFK")
                        .HasColumnType("int");

                    b.Property<int>("TransacaoFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtigoFK");

                    b.HasIndex("TransacaoFK");

                    b.ToTable("Transacao_Artigo");
                });

            modelBuilder.Entity("futshop_dweb.Models.Utilizador", b =>
                {
                    b.Property<int>("UtilizadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UtilizadorId"));

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("bit");

                    b.Property<string>("Telemovel")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("codigopostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("morada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UtilizadorId");

                    b.ToTable("Utilizadores");

                    b.HasData(
                        new
                        {
                            UtilizadorId = 1,
                            Cidade = "Sistema",
                            DataNascimento = new DateOnly(2024, 7, 14),
                            Email = "sistema@gmail.com",
                            IsAdmin = true,
                            Nome = "Sistema",
                            Pais = "Portugal",
                            Password = "Admin123",
                            RememberMe = false,
                            Telemovel = "919999999",
                            codigopostal = "4000-000",
                            morada = "Sistema"
                        },
                        new
                        {
                            UtilizadorId = 2,
                            Cidade = "a",
                            DataNascimento = new DateOnly(2024, 7, 14),
                            Email = "a@a.com",
                            IsAdmin = false,
                            Nome = "a",
                            Pais = "Portugal",
                            Password = "12345",
                            RememberMe = false,
                            Telemovel = "919999999",
                            codigopostal = "4000-000",
                            morada = "a"
                        });
                });

            modelBuilder.Entity("DW_Final_Project.Models.Artigos", b =>
                {
                    b.HasOne("futshop_dweb.Models.Categoria", "Categoria")
                        .WithMany("Artigos")
                        .HasForeignKey("CategoriaFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("futshop_dweb.Models.Transacao", b =>
                {
                    b.HasOne("futshop_dweb.Models.Utilizador", "Utilizador")
                        .WithMany()
                        .HasForeignKey("UtilizadorFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("futshop_dweb.Models.Transacao_Artigo", b =>
                {
                    b.HasOne("DW_Final_Project.Models.Artigos", "Artigo")
                        .WithMany()
                        .HasForeignKey("ArtigoFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("futshop_dweb.Models.Transacao", "Transacao")
                        .WithMany()
                        .HasForeignKey("TransacaoFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artigo");

                    b.Navigation("Transacao");
                });

            modelBuilder.Entity("futshop_dweb.Models.Categoria", b =>
                {
                    b.Navigation("Artigos");
                });
#pragma warning restore 612, 618
        }
    }
}
