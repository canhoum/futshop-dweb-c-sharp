using DW_Final_Project.Models;

namespace futshop_dweb.Models
{
    public class Global
    {
        public static Utilizador LoggedUser { get; set; } = null;

        public static List<Carrinho> Carrinho { get; set; } = new List<Carrinho>();

        /*
        Global.Carrinho.Add(new Carrinho
        {
            Artigo = produto,
            Quantidade = quantidade
        });


        Global.Carrinho.Remove(Global.Carrinho.Where(c => c.Artigo.Id == id).FirstOrDefault());


        foreach (var item in Global.Carrinho)
        {
            _context.-.....(item);
        }

        
        */
    }
}
