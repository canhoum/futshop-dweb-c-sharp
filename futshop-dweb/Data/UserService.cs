namespace futshop_dweb.Data
{
    using futshop_dweb.Models;
    // UserService.cs
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    /// <summary>
    /// Objeto com serviços de controlo da aplicação
    /// </summary>
    public class UserService : IUserService
    {
        public bool IsAuthenticated => Global.LoggedUser != null;

        public bool IsAdmin => Global.LoggedUser != null && Global.LoggedUser.IsAdmin ==true;

        public bool addedToCart => Global.addedToCart == true;

        public bool finishedOrder => Global.finishedOrder == true;
        public bool noItems => Global.Carrinho.Count == 0;

        public void resetFinishedOrder()
        {
            Global.finishedOrder = false;
        }
        
        public void rmCart()
        {
            Global.addedToCart = false;
        }
        public void removeCart(int id) // Adicione este método
        {
            var artigo = Global.Carrinho.FirstOrDefault(c => c.Artigo.Id == id);
            if (artigo != null)
            {
                Global.Carrinho.Remove(artigo);
                Global.addedToCart = false;
            }
        }

        public int getUserID()
        {
            return Global.LoggedUser.UtilizadorId;
        }
    }

}
