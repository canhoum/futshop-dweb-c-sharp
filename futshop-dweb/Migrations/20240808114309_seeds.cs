using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace futshop_dweb.Migrations
{
    /// <inheritdoc />
    public partial class seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artigos",
                columns: new[] { "Id", "CategoriaFK", "Descricao", "ImagemURL", "Nome", "Preco", "Quantidade", "Tamanho" },
                values: new object[,]
                {
                    { 1, 1, "Camisola do Benfica Oficial", "", "Camisola Benfica", 20.0, 10, "M" },
                    { 2, 2, "Camisola do Manchester City Oficial", "", "Camisola Manchester City", 22.0, 20, "L" },
                    { 3, 3, "Camisola do Real Madrid Oficial", "", "Camisola Real Madrid", 18.0, 50, "L" },
                    { 4, 4, "Camisola do Benfica Retro Oficial", "", "Camisola Benfica Retro", 50.0, 19, "S" }
                });

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 1,
                column: "DataNascimento",
                value: new DateOnly(2024, 8, 8));

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 2,
                column: "DataNascimento",
                value: new DateOnly(2024, 8, 8));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artigos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artigos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Artigos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artigos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 1,
                column: "DataNascimento",
                value: new DateOnly(2024, 7, 14));

            migrationBuilder.UpdateData(
                table: "Utilizadores",
                keyColumn: "UtilizadorId",
                keyValue: 2,
                column: "DataNascimento",
                value: new DateOnly(2024, 7, 14));
        }
    }
}
