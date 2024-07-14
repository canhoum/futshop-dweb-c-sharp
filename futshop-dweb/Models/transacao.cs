using DW_Final_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class Transacao
    {
        public Transacao()
        {
        }
        public Transacao(decimal t, int ufk)
        {
            CompraId = 0;
            DataCompra = DateTime.Now;
            Total=t;
            UtilizadorFK = ufk;
        }
    
        [Key]
        public int CompraId { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal Total { get; set; }

        [ForeignKey (nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }
    }
}
