using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Data;
using DW_Final_Project.Models;
using futshop_dweb.Models;


namespace futshop_dweb.Controllers
{
    public class ArtigosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artigos/IndexUsers
        //Lógica para os users
        public async Task<IActionResult> IndexUsers()
        {
            var applicationDbContext = _context.Artigos.Include(a => a.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> addCarrinho(int? id)
        {
            var applicationDbContext = _context.Artigos.Include(a => a.Categoria);
            var artigo = await _context.Artigos
                .Include(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }
            Global.Carrinho.Add(new Carrinho(artigo,1));
            Global.addedToCart = true;
            return RedirectToAction("IndexUsers");
        }
        public IActionResult Carrinho()
        {
            var cartItems = Global.Carrinho; 
            return View(cartItems);
        }


        // GET: Artigos
        public async Task<IActionResult> Index()
        {
            //Verifica se o usuário está logado e se é um administrador, se não for admin retorna para a página inicial
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            var applicationDbContext = _context.Artigos.Include(a => a.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Verifica se o id é nulo. Se for, retorna um resultado de 'NotFound'.
            if (id == null)
            {
                return NotFound();
            }
            // Busca o artigo com o id especificado, incluindo a categoria relacionada.
            var artigos = await _context.Artigos
                .Include(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigos == null)
            {
                return NotFound();
            }

            return View(artigos);
        }

        // GET: Artigos/Create
        public IActionResult Create()
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome");
            return View();
        }

        // POST: Artigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Tamanho,Quantidade,PrecoAux,CategoriaFK")] Artigos artigos, IFormFile imageFile)
        {

            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
            // Remove as validações de 'Categoria' e 'Preco'.
            ModelState.Remove("Categoria");
            ModelState.Remove("Preco");
            if (ModelState.IsValid)
            {
                

                if (!string.IsNullOrEmpty(artigos.PrecoAux))
                {
                    

                    if (decimal.TryParse(artigos.PrecoAux, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal preco))
                    {
                                           artigos.Preco = Convert.ToDouble(preco);
                    }
                    else
                    {

                        // Adiciona um erro ao ModelState se 'PrecoAux' estiver vazio e retorna a visão com os dados atuais.
                        ModelState.AddModelError("", "O preço do artigo é inválido.");
                        ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
                        return View(artigos);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Deve escolher o preço do artigo, por favor.");
                    ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
                    return View(artigos);
                }
                // Gera um nome único para o arquivo e salva-o na pasta 'wwwroot/images'.
                if (imageFile != null && imageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    artigos.ImagemURL = fileName;
                }
                else
                {
                    artigos.ImagemURL = "default-c.png";
                }
                // Adiciona o novo artigo ao context e salva as mudanças na base de dados.
                _context.Artigos.Add(artigos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
            return View(artigos);

          
        }

        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos == null)
            {
                return NotFound();
            }

            artigos.PrecoAux = artigos.Preco.ToString();
            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Tamanho,Quantidade,PrecoAux,CategoriaFK")] Artigos artigos, IFormFile imageFile)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.Remove("Categoria");
            ModelState.Remove("Preco");
            if (id != artigos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingArtigo = await _context.Artigos.FindAsync(id);
                existingArtigo.Nome = artigos.Nome;
                existingArtigo.Descricao = artigos.Descricao;
                existingArtigo.Tamanho = artigos.Tamanho;
                existingArtigo.Quantidade = artigos.Quantidade;

                if (!string.IsNullOrEmpty(artigos.PrecoAux))
                {
                    if (decimal.TryParse(artigos.PrecoAux, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal preco))
                    {
                  
                        existingArtigo.Preco = Convert.ToDouble(preco);
                    }
                    else
                    {
                        ModelState.AddModelError("", "O preço do artigo é inválido.");
                        ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
                        return View(artigos);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Deve escolher o preço do artigo, por favor.");
                    ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
                    return View(artigos);
                }

                if (imageFile != null && imageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    existingArtigo.ImagemURL = fileName;
                }

                try
                {
                    _context.Update(existingArtigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigosExists(artigos.Id))
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
            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
            return View(artigos);
        }

        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos
                .Include(a => a.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigos == null)
            {
                return NotFound();
            }

            return View(artigos);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos != null)
            {
                _context.Artigos.Remove(artigos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigosExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
