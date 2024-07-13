﻿using DW_Final_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace futshop_dweb.Models
{
    public class Transacao_Artigo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey (nameof(Transacao))]
        public int TransacaoFK { get; set; }
        public Transacao Transacao { get; set; }
        
        [ForeignKey (nameof(Artigos))]
        public int ArtigoFK { get; set; }
        public Artigos Artigo { get; set; }
    }
}
