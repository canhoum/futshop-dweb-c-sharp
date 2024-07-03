using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class Transacao
    {
        [Key]
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }

        [ForeignKey (nameof(UtilizadorId))]
        public int UtilizadorFK { get; set; }
        public Utilizador UtilizadorId { get; set; }
        public ICollection<Carrinho> ItensCarrinho { get; set; }
    }
}
