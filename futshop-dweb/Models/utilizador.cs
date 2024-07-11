using DW_Final_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class Utilizador
    {
        public Utilizador()
        {
            Transacao = new HashSet<Transacao>();
        }

        /// <summary>
        /// Chave Primária (PK)
        /// </summary>
        [Key]
        public int UtilizadorId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        public string Nome { get; set; }
        /// <summary>
        /// número de telemóvel do Utilizador
        /// </summary>
        [Display(Name = "Telemóvel")]
        [StringLength(9)]
        //Pode inserir da forma +351, 00351, 963826342 
        [RegularExpression("9[1236][0-9]{7}",
             ErrorMessage = "o {0} só aceita 9 digitos")]
        public string Telemovel { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Display(Name = "Data de Nascimento")]
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Display(Name = "Morada")]
        public string morada { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Código Postal")]
        public string codigopostal { get; set; }


        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório.")]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

         // Novo atributo para indicar se o utilizador é administrador
        public bool IsAdmin { get; set; } = false;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [NotMapped]
        public string DataNascFormatted => DataNascimento.ToString("dd/MM/yyyy");

        // Relação um-para-muitos com Transacao
        public ICollection<Transacao> Transacao { get; set; }

        // Relação um-para-um com Carrinho
        public Carrinho Carrinho { get; set; }
    }
}