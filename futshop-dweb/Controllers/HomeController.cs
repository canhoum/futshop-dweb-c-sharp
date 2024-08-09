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

        /// <summary>
        /// Inicializa o <see cref="HomeController"/> com os serviços necessários.
        /// </summary>
        /// <param name="logger">Serviço de log para registrar eventos.</param>
        /// <param name="userService">Serviço para operações relacionadas a usuários.</param>
        /// <param name="context">Contexto de dados para acesso ao banco de dados.</param>
        public HomeController(ILogger<HomeController> logger, IUserService userService, ApplicationDbContext context)
        {
            _logger = logger;
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Exibe a página inicial. Verifica se o usuário está autenticado com base no cookie de autenticação.
        /// </summary>
        /// <returns>Retorna a View correspondente à página inicial.</returns>
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

        /// <summary>
        /// Exibe a página de privacidade.
        /// </summary>
        /// <returns>Retorna a View da página de privacidade.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Exibe a página de erro com informações sobre o request.
        /// </summary>
        /// <returns>Retorna a View da página de erro com o identificador do request.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Exibe a página de informações.
        /// </summary>
        /// <returns>Retorna a View da página de informações.</returns>
        public IActionResult Info()
        {
            bool isAuthenticated = _userService.IsAuthenticated;
            return View();
        }
    }
}
