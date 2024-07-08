using DW_Final_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class Transacao
    {
        public Transacao()
        {
        ArtigosList= new HashSet<Artigos>();
    }
    
        [Key]
        public int CompraId { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Total { get; set; }

        [ForeignKey (nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }
        public ICollection<Artigos> ArtigosList { get; set; }
    }
}
