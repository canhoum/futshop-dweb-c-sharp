using futshop_dweb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DW_Final_Project.Models
{
    public class Artigos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ -]+$", ErrorMessage = "Tem de escrever um {0} válido")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [RegularExpression("^[a-zçãõáéíóúA-ZÇÃÕÁÉÍÓÚ -.,]+$", ErrorMessage = "Tem de escrever uma {0} válida")]
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [RegularExpression("^(S|M|L|XL|XXL)$", ErrorMessage = "O {0} deve ser um dos seguintes valores: S, M, L, XL ou XXL.")]
        [Display(Name = "Tamanho")]
        public string Tamanho { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [Display(Name = "Quantidade")]
        [Range(0, int.MaxValue, ErrorMessage = "A {0} mínima é 0.")]
        public int Quantidade { get; set; }


        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")]
        [Display(Name = "Preço")]
        [RegularExpression("[0-9]+[.,]?[0-9]{1,2}", ErrorMessage = "No {0} só pode usar algarismos, e se desejar, duas casas decimais no final.")]
        public double Preco { get; set; }
        
        [NotMapped]
        public string PrecoAux { get; set; }

        public string? ImagemURL { get; set; }

        // Relação muitos-para-um com Categoria
        [ForeignKey(nameof(Categoria))]
        public int CategoriaFK { get; set; }

        public Categoria Categoria { get; set; }



    }
}
