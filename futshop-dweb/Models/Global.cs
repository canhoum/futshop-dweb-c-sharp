using DW_Final_Project.Models;

namespace futshop_dweb.Models
{
    public class Global
    {
        public static Utilizador LoggedUser { get; set; } = null;

        public static List<Carrinho> Carrinho { get; set; } = new List<Carrinho>();

        public static bool addedToCart { get; set; } = false;

       
    }
}
