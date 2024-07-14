namespace futshop_dweb.Data
{
    using futshop_dweb.Models;
    // UserService.cs
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => Global.LoggedUser != null;

        public bool IsAdmin => Global.LoggedUser != null && Global.LoggedUser.IsAdmin ==true;

        public bool addedToCart => Global.addedToCart == true;

        public bool finishedOrder => Global.finishedOrder == true;

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
