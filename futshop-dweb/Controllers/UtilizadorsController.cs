using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Data;
using futshop_dweb.Models;
using RestSharp;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizadores.ToListAsync());
        }

        // GET: Utilizadors/Details/5
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
            ViewBag.IsAdmin = utilizador.IsAdmin;

            return View(utilizador);

        }


        // GET: Utilizadors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilizadors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtilizadorId,Nome,Telemovel,DataNascimento,Email,Password,morada,codigopostal,Cidade,Pais")] Utilizador utilizador)
        {
            ModelState.Remove("Carrinho");
            if (ModelState.IsValid)
            {
                _context.Add(utilizador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }
            return View(utilizador);
        }

        // POST: Utilizadors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtilizadorId,Nome,Telemovel,DataNascimento,Email,morada,codigopostal,Cidade,Pais")] Utilizador utilizador)
        {
            if (id != utilizador.UtilizadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizador);
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
                return RedirectToAction(nameof(Index));
            }
            return View(utilizador);
        }

        // GET: Utilizadors/Delete/5
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
        [HttpGet]
        public IActionResult Register()
        {
            return View("Create");
        }

        // POST: Utilizadors/Register
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Register([Bind("Nome,Telemovel,DataNascimento,Email,morada,codigopostal,Cidade,Pais,Password")] Utilizador model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = new Utilizador
        //         {

        //             Email = model.Email,
        //             Nome = model.Nome,
        //             Telemovel = model.Telemovel,
        //             DataNascimento = model.DataNascimento,
        //             morada = model.morada,
        //             codigopostal = model.codigopostal,
        //             Cidade = model.Cidade,
        //             Pais = model.Pais
        //         };
        //         var result = await _userManager.CreateAsync(user, model.Password);
        //         if (result.Succeeded)
        //         {
        //             await _signInManager.SignInAsync(user, isPersistent: false);
        //             return RedirectToAction("Index", "Home");
        //         }
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError(string.Empty, error.Description);
        //         }
        //     }
        //     return View(model);
        // }

        // GET: Utilizadors/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // // POST: Utilizadors/Login
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
                    // Assuming you have some data you want to cache
                    Global.LoggedUser = result;
                    bool isAuthenticated = _context.IsUserLoggedIn();
                    // Armazenando informações na HttpContext.Items
                    HttpContext.Items["IsAuthenticated"] = isAuthenticated;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Global.LoggedUser = null;
            return RedirectToAction("Index", "Home");
        }
    }

}