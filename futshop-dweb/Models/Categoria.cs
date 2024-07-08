using DW_Final_Project.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace futshop_dweb.Models
{
    public class Categoria
    {
        [Key] public int Id { get; set; }

        public string Nome { get; set; }

        // Relação um-para-muitos com Artigos
        public ICollection<Artigos> Artigos { get; set; }
    }
}
