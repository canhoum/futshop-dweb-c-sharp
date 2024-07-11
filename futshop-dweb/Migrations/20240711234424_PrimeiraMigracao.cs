using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "carrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carrinho_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "UtilizadorId",
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
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.CompraId);
                    table.ForeignKey(
                        name: "FK_Transacao_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "UtilizadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoArtigo",
                columns: table => new
                {
                    CarrinhoFK = table.Column<int>(type: "int", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoArtigo", x => new { x.CarrinhoFK, x.ArtigoFK });
                    table.ForeignKey(
                        name: "FK_CarrinhoArtigo_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoArtigo_carrinho_CarrinhoFK",
                        column: x => x.CarrinhoFK,
                        principalTable: "carrinho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "UtilizadorId", "Cidade", "DataNascimento", "Email", "IsAdmin", "Nome", "Pais", "Password", "RememberMe", "Telemovel", "codigopostal", "morada" },
                values: new object[] { 1, "Sistema", new DateOnly(2024, 7, 12), "sistema@gmail.com", true, "Sistema", "Portugal", "Admin123", false, "919999999", "4000-000", "Sistema" });

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_CategoriaFK",
                table: "Artigos",
                column: "CategoriaFK");

            migrationBuilder.CreateIndex(
                name: "IX_carrinho_UtilizadorFK",
                table: "carrinho",
                column: "UtilizadorFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoArtigo_ArtigoFK",
                table: "CarrinhoArtigo",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_ArtigoFK",
                table: "Transacao",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_UtilizadorFK",
                table: "Transacao",
                column: "UtilizadorFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoArtigo");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "carrinho");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
