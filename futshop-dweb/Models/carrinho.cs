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
        public int UtilizadorId { get; set; }
        public Utilizador Utilizador { get; set; }

        public ICollection<CarrinhoArtigo> Artigos { get; set; }

        [NotMapped]
        public decimal PrecoTotal
        {
            get
            {
                decimal total = 0;

                foreach (var carrinhoArtigo in Artigos)
                {
                    total += Convert.ToDecimal(carrinhoArtigo.Artigos.PrecoAux) * carrinhoArtigo.Quantidade;
                }

                return total;
            }
        }


        // Métodos para manipulação do carrinho
        public void AddToCarrinho(int artigoId, int quantidade, ApplicationDbContext context)
        {
            var artigo = Artigos.FirstOrDefault(a => a.ArtigoId == artigoId);

            if (artigo == null)
            {
                Artigos.Add(new CarrinhoArtigo
                {
                    ArtigoId = artigoId,
                    Quantidade = quantidade
                });
            }
            else
            {
                artigo.Quantidade += quantidade;
            }

            context.SaveChanges();
        }

        public void RemoveFromCarrinho(int artigoId, ApplicationDbContext context)
        {
            var artigo = Artigos.FirstOrDefault(a => a.ArtigoId == artigoId);

            if (artigo != null)
            {
                Artigos.Remove(artigo);
                context.SaveChanges();
            }
        }

        public IEnumerable<CarrinhoArtigo> ListArtigos()
        {
            return Artigos;
        }
    }
}