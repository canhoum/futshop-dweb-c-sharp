using futshop_dweb.Data;
using futshop_dweb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TransacaosController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransacaosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Transacaos
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Transacao.Include(t => t.Utilizador);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Transacaos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        //Verifica as transações feitas pelo utilizador, s
        var transacao_artigo = await _context.Transacao_Artigo
            .Include(t => t.Artigo)
            .Include(t => t.Transacao)
            .Where(t => t.TransacaoFK == id).ToListAsync();
        if (transacao_artigo == null)
        {
            return View("Index");
        }

        return View(transacao_artigo);
    }

    // GET: Transacaos/Create
    public IActionResult Create()
    {
        ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "UtilizadorId", "Cidade");
        return View();
    }

    // POST: Transacaos/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CompraId,DataCompra,Total,UtilizadorFK")] Transacao transacao)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "UtilizadorId", "Cidade", transacao.UtilizadorFK);
        return View(transacao);
    }

    // GET: Transacaos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacao = await _context.Transacao.FindAsync(id);
        if (transacao == null)
        {
            return NotFound();
        }
        ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "UtilizadorId", "Cidade", transacao.UtilizadorFK);
        return View(transacao);
    }

    // POST: Transacaos/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CompraId,DataCompra,Total,UtilizadorFK")] Transacao transacao)
    {
        if (id != transacao.CompraId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransacaoExists(transacao.CompraId))
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
        ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "UtilizadorId", "Cidade", transacao.UtilizadorFK);
        return View(transacao);
    }

    // GET: Transacaos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacao = await _context.Transacao
            .Include(t => t.Utilizador)
            .FirstOrDefaultAsync(m => m.CompraId == id);
        if (transacao == null)
        {
            return NotFound();
        }

        return View(transacao);
    }

    // POST: Transacaos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transacao = await _context.Transacao.FindAsync(id);
        if (transacao != null)
        {
            _context.Transacao.Remove(transacao);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransacaoExists(int id)
    {
        return _context.Transacao.Any(e => e.CompraId == id);
    }

    // GET: Transacaos/TransacoesPorUtilizador
    public async Task<IActionResult> TransacoesPorUtilizador(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var transacoes = await _context.Transacao
            .Include(t => t.Utilizador)
            .Where(t => t.UtilizadorFK == id)
            .ToListAsync();

        if (transacoes == null || transacoes.Count == 0)
        {
            return NotFound();
        }

        return View("Index",transacoes);
    }

    public async Task<IActionResult> FinalizarCompra()
    {
        decimal total = 0;
        foreach (var item in Global.Carrinho)
        {
            total += Convert.ToDecimal(item.Artigo.Preco) * item.Quantidade;
        }

        Transacao transacao = new Transacao(total,Global.LoggedUser.UtilizadorId);
        _context.Transacao.Add(transacao);
        await _context.SaveChangesAsync();
        //receber o ultimo resutlado da transacao
        transacao = await _context.Transacao.OrderByDescending(t => t.CompraId).FirstOrDefaultAsync();
        foreach (var item in Global.Carrinho)
        {
            Transacao_Artigo transacao_Artigo = new Transacao_Artigo(item.Artigo.Id,transacao.CompraId);
            _context.Transacao_Artigo.Add(transacao_Artigo);
            await _context.SaveChangesAsync();
        }
        Global.finishedOrder = true;
        Global.Carrinho.Clear();
        return RedirectToAction("Carrinho","Artigos");
    }
}
