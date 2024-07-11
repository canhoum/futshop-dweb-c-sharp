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
    
        [Key]
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }

        [ForeignKey (nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }


        [ForeignKey(nameof(Artigo))]
        public int ArtigoFK { get; set; }
        public Artigos Artigo { get; set; }
    }
}
