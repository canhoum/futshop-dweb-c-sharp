using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Data;
using futshop_dweb.Models;

namespace futshop_dweb.Controllers
{
    public class Transacao_ArtigoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Transacao_ArtigoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transacao_Artigo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transacao_Artigo.Include(t => t.Transacao);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transacao_Artigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao_Artigo = await _context.Transacao_Artigo
                .Include(t => t.Transacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao_Artigo == null)
            {
                return NotFound();
            }

            return View(transacao_Artigo);
        }

        // GET: Transacao_Artigo/Create
        public IActionResult Create()
        {
            ViewData["TransacaoFK"] = new SelectList(_context.Transacao, "CompraId", "CompraId");
            return View();
        }

        // POST: Transacao_Artigo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransacaoFK,ArtigoFK")] Transacao_Artigo transacao_Artigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacao_Artigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransacaoFK"] = new SelectList(_context.Transacao, "CompraId", "CompraId", transacao_Artigo.TransacaoFK);
            return View(transacao_Artigo);
        }

        // GET: Transacao_Artigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao_Artigo = await _context.Transacao_Artigo.FindAsync(id);
            if (transacao_Artigo == null)
            {
                return NotFound();
            }
            ViewData["TransacaoFK"] = new SelectList(_context.Transacao, "CompraId", "CompraId", transacao_Artigo.TransacaoFK);
            return View(transacao_Artigo);
        }

        // POST: Transacao_Artigo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TransacaoFK,ArtigoFK")] Transacao_Artigo transacao_Artigo)
        {
            if (id != transacao_Artigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacao_Artigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Transacao_ArtigoExists(transacao_Artigo.Id))
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
            ViewData["TransacaoFK"] = new SelectList(_context.Transacao, "CompraId", "CompraId", transacao_Artigo.TransacaoFK);
            return View(transacao_Artigo);
        }

        // GET: Transacao_Artigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao_Artigo = await _context.Transacao_Artigo
                .Include(t => t.Transacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao_Artigo == null)
            {
                return NotFound();
            }

            return View(transacao_Artigo);
        }

        // POST: Transacao_Artigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transacao_Artigo = await _context.Transacao_Artigo.FindAsync(id);
            if (transacao_Artigo != null)
            {
                _context.Transacao_Artigo.Remove(transacao_Artigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Transacao_ArtigoExists(int id)
        {
            return _context.Transacao_Artigo.Any(e => e.Id == id);
        }
    }
}
