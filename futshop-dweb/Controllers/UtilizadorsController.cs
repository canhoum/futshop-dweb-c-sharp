using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Data;
using futshop_dweb.Models;
using RestSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace futshop_dweb.Controllers
{
    public class UtilizadorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilizadorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilizadors
        /// <summary>
        /// Método responsável por exibir a lista de utilizadores. 
        /// Redireciona para a página de login se o utilizador não estiver logado, 
        /// ou para a página inicial se o utilizador logado não for administrador.
        /// </summary>
        /// <returns>Retorna a View "Index" com a lista de utilizadores, ou redireciona para a View "Login" se o utilizador não estiver logado,
        /// ou para a View "Home/Index" se o utilizador não for administrador.</returns>
        public async Task<IActionResult> Index()
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(await _context.Utilizadores.ToListAsync());
        }

        // GET: Utilizadors/Details/5
        /// <summary>
        /// Método responsável por exibir os detalhes de um utilizador específico.
        /// </summary>
        /// <param name="id">O identificador único do utilizador cujos detalhes serão exibidos.</param>
        /// <returns>Retorna a View com os detalhes do utilizador, ou "NotFound" se o utilizador não for encontrado.</returns>
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.UtilizadorId == id);
            if (utilizador == null)
            {
                return NotFound();
            }
            ViewBag.IsAdmin = Global.LoggedUser.IsAdmin;

            return View(utilizador);

        }

        public async Task<IActionResult> VerPerfil(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.UtilizadorId == id);
            if (utilizador == null)
            {
                return NotFound();
            }
            ViewBag.IsAdmin = utilizador.IsAdmin;

            return View("Details", utilizador);

        }


        // GET: Utilizadors/Create
        /// <summary>
        ///  Método responsável por exibir a View de criação de um novo utilizador.
        /// </summary>
        /// <returns>Retorna a View "Create" com informações adicionais no ViewBag, como o status de administrador do utilizador logado.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.IsAdmin = Global.LoggedUser.IsAdmin;
            return View();
        }

        // POST: Utilizadors/Create
        /// <summary>
        /// Método responsável por processar a criação de um novo utilizador.
        /// </summary>
        /// <param name="utilizador">Objeto que contém os dados do novo utilizador a ser criado.</param>
        /// <returns>Redireciona para a View "Index" em caso de sucesso, ou para a página inicial ("Home/Index") 
        /// em caso de falha na validação do modelo.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilizadorId,Nome,Telemovel,DataNascimento,Email,Password,morada,codigopostal,Cidade,Pais,IsAdmin")] Utilizador utilizador)
        {
            ModelState.Remove("Carrinho");
            if (ModelState.IsValid)
            {
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Utilizadors/Edit/5
        /// <summary>
        ///  Método responsável por exibir a View de edição de um utilizador específico.
        /// </summary>
        /// <param name="id">O identificador único do utilizador a ser editado.</param>
        /// <returns>Retorna a View com os detalhes do utilizador para edição, ou "NotFound" se o utilizador não for encontrado.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        /// <summary>
        /// Método responsável por processar a edição de um utilizador existente.
        /// </summary>
        /// <param name="id">O identificador único do utilizador a ser editado.</param>
        /// <param name="utilizador"> Objeto que contém os dados atualizados do utilizador.</param>
        /// <returns>Redireciona para a View "Index" em caso de sucesso, ou retorna a View "Edit" com o modelo atual 
        /// caso ocorra alguma falha na validação ou na atualização.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilizadorId,Nome,Telemovel,DataNascimento,Email,morada,codigopostal,Cidade,Pais")] Utilizador utilizador)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUtilizador = await _context.Utilizadores.FindAsync(id);
                    string pwd = existingUtilizador.Password;

                    // Update the fields you want to change
                    existingUtilizador.Nome = utilizador.Nome;
                    existingUtilizador.Telemovel = utilizador.Telemovel;
                    existingUtilizador.DataNascimento = utilizador.DataNascimento;
                    existingUtilizador.Email = utilizador.Email;
                    existingUtilizador.morada = utilizador.morada;
                    existingUtilizador.codigopostal = utilizador.codigopostal;
                    existingUtilizador.Cidade = utilizador.Cidade;
                    existingUtilizador.Pais = utilizador.Pais;

                    // Reassign the preserved password
                    existingUtilizador.Password = pwd;

                    _context.Update(existingUtilizador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorExists(utilizador.UtilizadorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("VerPerfil", new { id = utilizador.UtilizadorId });
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
        /// <summary>
        /// Método responsável por exibir a View de confirmação para exclusão de um utilizador específico.
        /// </summary>
        /// <param name="id">O identificador único do utilizador a ser excluído.</param>
        /// <returns>Retorna a View com os detalhes do utilizador para confirmação da exclusão, 
        /// ou "NotFound" se o utilizador não for encontrado.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FirstOrDefaultAsync(m => m.UtilizadorId == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Utilizadors/Delete/5

        /// <summary>
        /// Método responsável por confirmar e realizar a exclusão de um utilizador.
        /// </summary>
        /// <param name="id">O identificador único do utilizador a ser excluído.</param>
        /// <returns> Redireciona para a View "Index" após a exclusão do utilizador.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador != null)
            {
                _context.Utilizadores.Remove(utilizador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.UtilizadorId == id);
        }

        // GET: Utilizadors/Register
        /// <summary>
        ///  Método responsável por exibir a View de registro de um novo utilizador.
        /// </summary>
        /// <returns>Retorna a View "Create" para o registro de um novo utilizador.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View("Create");
        }

        // GET: Utilizadors/Login
        /// <summary>
        ///  Método responsável por exibir a View de login.
        /// </summary>
        /// <returns>Retorna a View "Login" para que o utilizador possa autenticar-se.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //POST: Utilizadors/Login
        /// <summary>
        /// Método responsavel pelo Login.
        /// </summary>
        /// <param name="model">Objeto representativo de um utilizador passado na View</param>
        /// <returns>Retorna a View "Index" em caso de sucesso, ou, "Login" em caso de insucesso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password,RememberMe")] Utilizador model)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Telemovel");
            ModelState.Remove("UtilizadorId");
            ModelState.Remove("Pais");
            ModelState.Remove("Cidade");
            ModelState.Remove("codigopostal");
            ModelState.Remove("morada");
            ModelState.Remove("Carrinho");
            if (ModelState.IsValid)
            {
                var result = await _context.Utilizadores.FirstOrDefaultAsync(m => m.Email == model.Email);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
                else if (result.Password != model.Password)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
                else
                {                
                    Global.LoggedUser = result;
                    if (model.RememberMe)
                    {
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30), // Set expiration time for the cookie
                            Secure = true, // Use HTTPS
                            HttpOnly = true, // Cookie is not accessible via JavaScript
                            SameSite = SameSiteMode.Strict // Cookie is sent only in first-party contexts
                        };

                        // Store the user identifier in the cookie (replace with your own logic)
                        Response.Cookies.Append("UserAuthCookie",Global.LoggedUser.UtilizadorId.ToString(), cookieOptions);

                    }
                    bool isAuthenticated = _context.IsUserLoggedIn();
                    HttpContext.Items["IsAuthenticated"] = isAuthenticated;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Método responsável por processar o logout do utilizador atual.
        /// </summary>
        /// <returns>Redireciona para a View "Index" do controller "Home" após limpar a sessão do utilizador e remover o cookie de autenticação.</returns>
        [HttpGet]
        public IActionResult Logout()
        {
            Global.LoggedUser = null;
            // Remove the cookie by setting its expiration date to a past date
            Response.Cookies.Delete("UserAuthCookie");
            return RedirectToAction("Index", "Home");
        }
    }

}