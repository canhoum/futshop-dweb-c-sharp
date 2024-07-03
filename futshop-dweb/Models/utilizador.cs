using System.ComponentModel.DataAnnotations;

namespace futshop_dweb.Models
{
    public class Utilizador
    {
        [Key]
        public int UtilizadorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<Transacao> Transacao { get; set; }
    }
}
