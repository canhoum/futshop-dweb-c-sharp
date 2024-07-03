using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class transacao
    {
        [Key]
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }

        [ForeignKey (nameof(UtilizadorId))]
        public int UtilizadorFK { get; set; }
        public utilizador UtilizadorId { get; set; }
        public ICollection<carrinho> ItensCarrinho { get; set; }
    }
}
