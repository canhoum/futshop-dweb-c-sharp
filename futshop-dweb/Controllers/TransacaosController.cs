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
    /// <summary>
    /// Exibe a lista de todas as transações.
    /// </summary>
    /// <returns>Retorna uma view com a lista de transações, incluindo informações sobre o utilizador.</returns>
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Transacao.Include(t => t.Utilizador);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Transacaos/Details/5
    /// <summary>
    /// Exibe os detalhes de uma transação específica, incluindo os artigos relacionados a ela.
    /// </summary>
    /// <param name="id">O identificador único da transação.</param>
    /// <returns>Retorna uma view com os detalhes da transação e os artigos associados.</returns>
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        ViewBag.UtilizadorId = id;
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
    /// <summary>
    /// Exibe a view para criar uma nova transação.
    /// </summary>
    /// <returns>Retorna uma view com um formulário para criar uma nova transação.</returns>
    public IActionResult Create()
    {
        ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores, "UtilizadorId", "Cidade");
        return View();
    }

    // POST: Transacaos/Create
    /// <summary>
    /// Processa a criação de uma nova transação após o formulário ser enviado.
    /// </summary>
    /// <param name="transacao">O objeto Transacao contendo os dados da nova transação.</param>
    /// <returns>Retorna a view do formulário de criação se houver erros de validação, caso contrário, redireciona para o índice.</returns>
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
    /// <summary>
    /// Exibe a view para editar uma transação existente.
    /// </summary>
    /// <param name="id">O identificador único da transação a ser editada.</param>
    /// <returns>Retorna a view de edição com os dados da transação, ou NotFound se o ID não for válido.</returns>
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
    /// <summary>
    /// Processa a edição de uma transação existente após o formulário ser enviado.
    /// </summary>
    /// <param name="id">O identificador único da transação a ser editada.</param>
    /// <param name="transacao">O objeto Transacao contendo os dados atualizados.</param>
    /// <returns>Retorna a view do formulário de edição se houver erros de validação, caso contrário, redireciona para o índice.</returns> 
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
    /// <summary>
    /// Exibe a view para confirmar a exclusão de uma transação.
    /// </summary>
    /// <param name="id">O identificador único da transação a ser excluída.</param>
    /// <returns>Retorna a view de confirmação de exclusão ou NotFound se o ID não for válido.</returns>
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
    /// <summary>
    /// Processa a exclusão de uma transação após a confirmação no formulário.
    /// </summary>
    /// <param name="id">O identificador único da transação a ser excluída.</param>
    /// <returns>Redireciona para o índice após a exclusão bem-sucedida.</returns>
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

    /// <summary>
    /// Verifica se uma transação existe no banco de dados pelo ID.
    /// </summary>
    /// <param name="id">O identificador único da transação.</param>
    /// <returns>Retorna true se a transação existir, caso contrário, false.</returns>
    private bool TransacaoExists(int id)
    {
        return _context.Transacao.Any(e => e.CompraId == id);
    }

    // GET: Transacaos/TransacoesPorUtilizador
    /// <summary>
    /// Obtém as transações realizadas por um usuário específico.
    /// </summary>
    /// <param name="id">O identificador único do usuário.</param>
    /// <returns>Retorna uma view com a lista de transações do usuário ou NotFound se não houver transações.</returns>
    public async Task<IActionResult> TransacoesPorUtilizador(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        ViewBag.UtilizadorId = id;
        var transacoes = await _context.Transacao
            .Include(t => t.Utilizador)
            .Where(t => t.UtilizadorFK == id)
            .ToListAsync();

        // if (transacoes == null || transacoes.Count == 0)
        // {
        //     return NotFound();
        // }

        return View("Index",transacoes);
    }

    /// <summary>
    /// Finaliza a compra atual, criando uma nova transação e associando os itens do carrinho.
    /// </summary>
    /// <returns>Redireciona para a página de carrinho de artigos após a finalização da compra.</returns>
    public async Task<IActionResult> FinalizarCompra()
    {
        decimal total = 0;
        if (Global.Carrinho.Count == 0)
        {
            return RedirectToAction("Carrinho","Artigos");
        }
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
