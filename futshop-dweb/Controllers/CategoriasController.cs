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
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        /// <summary>
        /// Exibe a lista de todas as categorias.
        /// </summary>
        /// <returns>Retorna a View com a lista de categorias. Se o usuário não estiver logado ou não for um administrador,
        /// redireciona para a página de login ou para a página inicial.</returns>
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
            return View(await _context.Categoria.ToListAsync());
        }
        /// <summary>
        /// Exibe os detalhes de uma categoria específica.
        /// </summary>
        /// <param name="id">Identificador único da categoria cujos detalhes serão exibidos.</param>
        /// <returns>Retorna a View com os detalhes da categoria especificada. 
        /// Se o usuário não estiver logado ou não for um administrador, redireciona para a página de login ou para a página inicial.
        /// Se o ID da categoria for nulo ou a categoria não for encontrada, retorna um resultado de 'NotFound'.</returns>

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
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

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        /// <summary>
        /// Exibe a página de criação de uma nova categoria.
        /// </summary>
        /// <returns> Retorna a View para criar uma nova categoria. 
        /// Se o usuário não estiver logado ou não for um administrador, redireciona para a página de login ou para a página inicial.</returns>
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

            return View();
        }

        // POST: Categorias/Create
        /// <summary>
        /// Cria uma nova categoria com base nos dados fornecidos.
        /// </summary>
        /// <param name="categoria">Objeto representativo da categoria a ser criada, contendo o nome e outros detalhes.</param>
        /// <returns>Redireciona para a lista de categorias se a criação for bem-sucedida. 
        /// Se o usuário não estiver logado ou não for um administrador, redireciona para a página de login ou para a página inicial.
        /// Se o modelo não for válido ou se uma categoria com o mesmo nome já existir, retorna a View de criação com mensagens de erro.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Categoria categoria)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // Verifica se já existe uma categoria com o mesmo nome
                var existingCategoria = await _context.Categoria
           .FirstOrDefaultAsync(c => c.Nome == categoria.Nome);

                if (existingCategoria != null)
                {
                    // Não deixa criar uma nova categoria caso já exista
                    ModelState.AddModelError("Nome", "Já existe uma categoria com esse nome.");
                    return View(categoria);
                }

                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        /// <summary>
        /// Exibe a página de edição para uma categoria existente, identificada pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria a ser editada.</param>
        /// <returns>Retorna a View de edição da categoria se a categoria for encontrada e o usuário estiver autenticado como administrador.
        /// Caso contrário, redireciona para a página de login ou para a página inicial se o usuário não for um administrador.
        /// Se o ID for nulo ou a categoria não for encontrada, retorna um resultado de 'NotFound'.</returns>
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

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        /// <summary>
        /// Atualiza uma categoria existente com as informações fornecidas.
        /// </summary>
        /// <param name="id">O ID da categoria a ser atualizada.</param>
        /// <param name="categoria">O objeto da categoria com os novos valores a serem aplicados.</param>
        /// <returns>Retorna uma redireção para a lista de categorias (Index) se a atualização for bem-sucedida.
        /// Caso contrário, retorna a mesma página de edição com os dados da categoria em caso de erro de validação ou concorrência.
        /// Se o ID fornecido não corresponder ao ID da categoria, retorna um resultado de 'NotFound'.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Categoria categoria)
        {
            if (Global.LoggedUser == null)
            {
                return RedirectToAction("Login", "Utilizadors");
            }
            else if (Global.LoggedUser.IsAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        /// <summary>
        /// Exibe a página de confirmação para a exclusão de uma categoria específica.
        /// </summary>
        /// <param name="id">O ID da categoria a ser excluída.</param>
        /// <returns>Retorna a visão de confirmação para a exclusão se a categoria for encontrada.
        /// Caso contrário, retorna um resultado de 'NotFound' se o ID não for encontrado ou for nulo.
        /// Se o usuário não estiver logado ou não for um administrador, redireciona para a página de login ou para a página inicial, respectivamente.</returns>
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

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        /// <summary>
        /// Processa a exclusão confirmada de uma categoria específica.
        /// </summary>
        /// <param name="id">O ID da categoria a ser excluída.</param>
        /// <returns>Redireciona para a ação 'Index' se a exclusão for bem-sucedida.
        /// Se o usuário não estiver logado, redireciona para a página de login.
        /// Se o usuário não for um administrador, redireciona para a página inicial.</returns>
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

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica se uma categoria com o ID especificado existe no contexto de dados.
        /// </summary>
        /// <param name="id"> O ID da categoria que será verificado.</param>
        /// <returns>Retorna true se uma categoria com o ID especificado existir; caso contrário, retorna false.</returns>
        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}
