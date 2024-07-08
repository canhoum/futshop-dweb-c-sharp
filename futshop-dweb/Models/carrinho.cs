using futshop_dweb.Data;
using futshop_dweb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DW_Final_Project.Models
{
    public class Carrinho
    {
        public Carrinho()
        {
            Artigos = new HashSet<CarrinhoArtigo>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Utilizador))]
        public int UtilizadorFK { get; set; }
        public Utilizador Utilizador { get; set; }

        public ICollection<CarrinhoArtigo> Artigos { get; set; }

    }
}