using futshop_dweb.Data;
using futshop_dweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace futshop_dweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ApplicationDbContext context)
        {
            _logger = logger;
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            // Check if the cookie exists
            var userIdentifier = Request.Cookies["UserAuthCookie"];

            if (!string.IsNullOrEmpty(userIdentifier))
            {
                var result = await _context.Utilizadores.FirstOrDefaultAsync(m => m.UtilizadorId == Convert.ToInt32(userIdentifier));
                Global.LoggedUser = result;
                bool isAuthenticated = _userService.IsAuthenticated;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Info()
        {
            bool isAuthenticated = _userService.IsAuthenticated;
            return View();
        }
    }
}
