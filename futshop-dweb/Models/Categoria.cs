using System.ComponentModel.DataAnnotations;

namespace futshop_dweb.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public ICollection<Artigos> Artigos { get; set; }
    
}
}
