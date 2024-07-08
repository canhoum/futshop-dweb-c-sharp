using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Models;
using futshop_dweb.Data;

namespace futshop_dweb.Controllers
{
    public class CarrinhoArtigoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoArtigoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarrinhoArtigoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CarrinhoArtigo.Include(c => c.Artigos).Include(c => c.Carrinho);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarrinhoArtigoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinhoArtigo = await _context.CarrinhoArtigo
                .Include(c => c.Artigos)
                .Include(c => c.Carrinho)
                .FirstOrDefaultAsync(m => m.CarrinhoFK == id);
            if (carrinhoArtigo == null)
            {
                return NotFound();
            }

            return View(carrinhoArtigo);
        }

        // GET: CarrinhoArtigoes/Create
        public IActionResult Create()
        {
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Descricao");
            ViewData["CarrinhoFK"] = new SelectList(_context.carrinho, "Id", "Id");
            return View();
        }

        // POST: CarrinhoArtigoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarrinhoFK,ArtigoFK,Quantidade")] CarrinhoArtigo carrinhoArtigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrinhoArtigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Descricao", carrinhoArtigo.ArtigoFK);
            ViewData["CarrinhoFK"] = new SelectList(_context.carrinho, "Id", "Id", carrinhoArtigo.CarrinhoFK);
            return View(carrinhoArtigo);
        }

        // GET: CarrinhoArtigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinhoArtigo = await _context.CarrinhoArtigo.FindAsync(id);
            if (carrinhoArtigo == null)
            {
                return NotFound();
            }
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Descricao", carrinhoArtigo.ArtigoFK);
            ViewData["CarrinhoFK"] = new SelectList(_context.carrinho, "Id", "Id", carrinhoArtigo.CarrinhoFK);
            return View(carrinhoArtigo);
        }

        // POST: CarrinhoArtigoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarrinhoFK,ArtigoFK,Quantidade")] CarrinhoArtigo carrinhoArtigo)
        {
            if (id != carrinhoArtigo.CarrinhoFK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrinhoArtigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarrinhoArtigoExists(carrinhoArtigo.CarrinhoFK))
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
            ViewData["ArtigoFK"] = new SelectList(_context.Artigos, "Id", "Descricao", carrinhoArtigo.ArtigoFK);
            ViewData["CarrinhoFK"] = new SelectList(_context.carrinho, "Id", "Id", carrinhoArtigo.CarrinhoFK);
            return View(carrinhoArtigo);
        }

        // GET: CarrinhoArtigoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrinhoArtigo = await _context.CarrinhoArtigo
                .Include(c => c.Artigos)
                .Include(c => c.Carrinho)
                .FirstOrDefaultAsync(m => m.CarrinhoFK == id);
            if (carrinhoArtigo == null)
            {
                return NotFound();
            }

            return View(carrinhoArtigo);
        }

        // POST: CarrinhoArtigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrinhoArtigo = await _context.CarrinhoArtigo.FindAsync(id);
            if (carrinhoArtigo != null)
            {
                _context.CarrinhoArtigo.Remove(carrinhoArtigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarrinhoArtigoExists(int id)
        {
            return _context.CarrinhoArtigo.Any(e => e.CarrinhoFK == id);
        }
    }
}
