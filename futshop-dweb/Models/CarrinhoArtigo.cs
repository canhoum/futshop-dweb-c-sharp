using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
    public class CarrinhoArtigo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Carrinho))]
        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        [ForeignKey(nameof(Artigos))]
        public int ArtigoId { get; set; }
        public Artigos Artigos { get; set; }

        public int Quantidade { get; set; }
    }
}
