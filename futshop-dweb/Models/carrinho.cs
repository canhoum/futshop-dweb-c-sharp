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
     public Carrinho(Artigos art,int qtd)
        {
            Artigo = art;
            Quantidade = qtd;
        }
        public Artigos Artigo { get; set; }
        public int Quantidade { get; set; }

    }
}