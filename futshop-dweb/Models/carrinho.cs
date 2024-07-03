using System.ComponentModel.DataAnnotations;

namespace futshop_dweb.Models
{
    public class carrinho
    {
        [Key]  
        public int CarrinhoId { get; set; }
        public int CompraId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public transacao Compra { get; set; }
        public Artigos artigo { get; set; }
    }
}
