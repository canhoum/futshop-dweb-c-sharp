using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace futshop_dweb.Migrations
{
    /// <inheritdoc />
    public partial class bd : Migration
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
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.UtilizadorId);
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
    name: "Artigos",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        // other columns
        CategoriaFK = table.Column<int>(type: "int", nullable: false),
        TransacaoFK = table.Column<int>(type: "int", nullable: false),
        CarrinhoFK = table.Column<int>(type: "int", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Artigos", x => x.Id);
        table.ForeignKey(
            name: "FK_Artigos_Categoria_CategoriaFK",
            column: x => x.CategoriaFK,
            principalTable: "Categoria",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction); // No action on delete
        table.ForeignKey(
            name: "FK_Artigos_Transacao_TransacaoFK",
            column: x => x.TransacaoFK,
            principalTable: "Transacao",
            principalColumn: "CompraId",
            onDelete: ReferentialAction.NoAction); // No action on delete
        table.ForeignKey(
            name: "FK_Artigos_carrinho_CarrinhoFK",
            column: x => x.CarrinhoFK,
            principalTable: "carrinho",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction); // No action on delete
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

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_CarrinhoFK",
                table: "Artigos",
                column: "CarrinhoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_CategoriaFK",
                table: "Artigos",
                column: "CategoriaFK");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_TransacaoFK",
                table: "Artigos",
                column: "TransacaoFK");

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
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.DropTable(
                name: "carrinho");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
