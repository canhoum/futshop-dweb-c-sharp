using DW_Final_Project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace futshop_dweb.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Artigos = new HashSet<Artigos>();
        }
        [Key] 
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; }

        // Relação um-para-muitos com Artigos
        public ICollection<Artigos> Artigos { get; set; }
    }
}
