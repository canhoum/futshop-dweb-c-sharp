using futshop_dweb.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
    [PrimaryKey(nameof(CarrinhoFK), nameof(ArtigoFK))]
    public class CarrinhoArtigo
    {
        

        [ForeignKey(nameof(Carrinho))]
        public int CarrinhoFK { get; set; }
        public Carrinho Carrinho { get; set; }

        [ForeignKey(nameof(Artigos))]
        public int ArtigoFK { get; set; }
        public Artigos Artigos { get; set; }

        public int Quantidade { get; set; }
    }
}
