using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace futshop_dweb.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    UtilizadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    morada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigopostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.UtilizadorId);
                });

            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    ImagemURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artigos_Categoria_CategoriaFK",
                        column: x => x.CategoriaFK,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.CompraId);
                    table.ForeignKey(
                        name: "FK_Transacao_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacao_Artigo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransacaoFK = table.Column<int>(type: "int", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao_Artigo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacao_Artigo_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Artigo_Transacao_TransacaoFK",
                        column: x => x.TransacaoFK,
                        principalTable: "Transacao",
                        principalColumn: "CompraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Liga Betclic" },
                    { 2, "Premier League" },
                    { 3, "La Liga" },
                    { 4, "Liga Betclic Retro" },
                    { 5, "Premier League Retro" },
                    { 6, "La Liga Retro" },
                    { 7, "Europa" },
                    { 8, "América" },
                    { 9, "África" }
                });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "UtilizadorId", "Cidade", "DataNascimento", "Email", "IsAdmin", "Nome", "Pais", "Password", "RememberMe", "Telemovel", "codigopostal", "morada" },
                values: new object[,]
                {
                    { 1, "Sistema", new DateOnly(2024, 7, 14), "sistema@gmail.com", true, "Sistema", "Portugal", "Admin123", false, "919999999", "4000-000", "Sistema" },
                    { 2, "a", new DateOnly(2024, 7, 14), "a@a.com", false, "a", "Portugal", "12345", false, "919999999", "4000-000", "a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_CategoriaFK",
                table: "Artigos",
                column: "CategoriaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_UtilizadorFK",
                table: "Transacao",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_Artigo_ArtigoFK",
                table: "Transacao_Artigo",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_Artigo_TransacaoFK",
                table: "Transacao_Artigo",
                column: "TransacaoFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacao_Artigo");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
