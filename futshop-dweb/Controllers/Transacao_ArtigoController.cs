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
        /// <summary>
        /// Exibe uma lista de todas as transações de artigos.
        /// </summary>
        /// <returns>Retorna a View com a lista de transações de artigos.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transacao_Artigo.Include(t => t.Transacao);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transacao_Artigo/Details/5
        /// <summary>
        /// Exibe os detalhes de uma transação de artigo específica.
        /// </summary>
        /// <param name="id">O identificador da transação de artigo a ser exibida.</param>
        /// <returns>Retorna a View com os detalhes da transação de artigo, ou uma página de erro se não for encontrado.</returns>
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
        /// <summary>
        /// Exibe o formulário para criar uma nova transação de artigo.
        /// </summary>
        /// <returns>Retorna a View com o formulário de criação de uma transação de artigo.</returns>
        public IActionResult Create()
        {
            ViewData["TransacaoFK"] = new SelectList(_context.Transacao, "CompraId", "CompraId");
            return View();
        }

        // POST: Transacao_Artigo/Create
        /// <summary>
        ///  Processa a submissão do formulário de criação de uma nova transação de artigo.
        /// </summary>
        /// <param name="transacao_Artigo">Objeto contendo os dados da transação de artigo a ser criada.</param>
        /// <returns>Redireciona para a ação Index se a criação for bem-sucedida; caso contrário, retorna a View com os dados atuais.</returns>

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
        /// <summary>
        /// Exibe a interface de edição para uma transação de artigo específica.
        /// </summary>
        /// <param name="id">O identificador único da transação de artigo a ser editada.</param>
        /// <returns>Retorna a View de edição com os dados da transação de artigo; se o id for nulo ou a transação não for encontrada, retorna NotFound.</returns>
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
        /// <summary>
        /// Processa a solicitação de edição de uma transação de artigo específica.
        /// </summary>
        /// <param name="id">O identificador único da transação de artigo a ser editada.</param>
        /// <param name="transacao_Artigo">O objeto Transacao_Artigo atualizado enviado pelo formulário.</param>
        /// <returns>Redireciona para a lista de transações de artigo se a atualização for bem-sucedida; 
        /// caso contrário, retorna a View de edição com os dados atuais e mensagens de erro, se houver.</returns>
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
        /// <summary>
        /// Exibe a página de confirmação para a exclusão de uma transação de artigo específica.
        /// </summary>
        /// <param name="id">O identificador único da transação de artigo a ser excluída.</param>
        /// <returns> Retorna a View de exclusão com os detalhes da transação de artigo se o id for válido e o objeto existir;
        /// caso contrário, retorna um erro de "Não encontrado"</returns>
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
        /// <summary>
        /// Executa a exclusão da transação de artigo especificada após a confirmação do usuário.
        /// </summary>
        /// <param name="id">O identificador único da transação de artigo a ser excluída.</param>
        /// <returns>Redireciona para a ação Index após a exclusão bem-sucedida ou 
        /// caso a transação de artigo não seja encontrada.</returns>
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

        /// <summary>
        /// Verifica se existe uma transação de artigo com o ID especificado.
        /// </summary>
        /// <param name="id">O identificador único da transação de artigo.</param>
        /// <returns>Retorna true se a transação de artigo existir; caso contrário, false.</returns>
        private bool Transacao_ArtigoExists(int id)
        {
            return _context.Transacao_Artigo.Any(e => e.Id == id);
        }
    }
}
