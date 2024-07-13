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

        public void rmCart()
        {
            Global.addedToCart = false;
        }   
    }

}
