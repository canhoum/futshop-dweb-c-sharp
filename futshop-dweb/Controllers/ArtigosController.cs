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

namespace futshop_dweb.Controllers
{
    public class ArtigosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artigos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artigos.Include(a => a.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        // GET: Artigos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome");
            return View();
        }

        // POST: Artigos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Tamanho,Quantidade,PrecoAux,CategoriaFK")] Artigos artigos, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(artigos.PrecoAux))
                {
                    if (decimal.TryParse(artigos.PrecoAux, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal preco))
                    {
                        // Se necessário, armazene o valor do preço em um campo apropriado
                    }
                    else
                    {
                        ModelState.AddModelError("", "O preço do artigo é inválido.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Deve escolher o preço do artigo, por favor.");
                }

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

                _context.Add(artigos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
            return View(artigos);
        }

        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos == null)
            {
                return NotFound();
            }

            artigos.PrecoAux = artigos.PrecoAux.ToString(CultureInfo.InvariantCulture);
            ViewData["CategoriaFK"] = new SelectList(_context.Categoria, "Id", "Nome", artigos.CategoriaFK);
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Tamanho,Quantidade,PrecoAux,CategoriaFK")] Artigos artigos, IFormFile imageFile)
        {
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
                        // Se necessário, armazene o valor do preço em um campo apropriado
                    }
                    else
                    {
                        ModelState.AddModelError("", "O preço do artigo é inválido.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Deve escolher o preço do artigo, por favor.");
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
