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
        /// <summary>
        /// Método responsável por exibir a lista de artigos para os utilizadores.
        /// </summary>
        /// <returns>Retorna a View com a lista de artigos, incluindo a categoria associada a cada artigo.</returns>
        public async Task<IActionResult> IndexUsers()
        {
            var applicationDbContext = _context.Artigos.Include(a => a.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// Método responsável por adicionar um artigo ao carrinho de compras do utilizador.
        /// </summary>
        /// <param name="id">O identificador único do artigo a ser adicionado ao carrinho.</param>
        /// <returns>Redireciona para a View "IndexUsers" após adicionar o artigo ao carrinho, 
        /// ou retorna "NotFound" se o artigo não for encontrado.</returns>
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

        /// <summary>
        /// Método responsável por exibir o conteúdo do carrinho de compras do utilizador.
        /// </summary>
        /// <returns>Retorna a View "Carrinho" com os itens atuais no carrinho de compras.</returns>
        public IActionResult Carrinho()
        {
            var cartItems = Global.Carrinho; 
            return View(cartItems);
        }

        /// <summary>
        ///  Método responsável por remover um artigo do carrinho de compras do utilizador.
        /// </summary>
        /// <param name="id"> O identificador único do artigo a ser removido do carrinho.</param>
        /// <returns>Redireciona para a View "Carrinho" após remover o artigo, 
        /// ou retorna "NotFound" se o artigo não for encontrado no carrinho.</returns>
        public IActionResult rmCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = Global.Carrinho.FirstOrDefault(c => c.Artigo.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            Global.Carrinho.Remove(artigo);
            Global.addedToCart = false;

            return RedirectToAction("Carrinho");
        }


        // GET: Artigos
        /// <summary>
        /// Método responsável por exibir a lista de artigos.
        /// Verifica se o utilizador está logado e se é um administrador; 
        /// se não for admin, redireciona para a página inicial.
        /// </summary>
        /// <returns>Retorna a View com a lista de artigos, incluindo a categoria associada a cada artigo,
        /// ou redireciona para a página de login ou para a página inicial, dependendo do status do utilizador.</returns>
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
        /// <summary>
        /// Método responsável por exibir os detalhes de um artigo específico.
        /// Verifica se o id é nulo e retorna um resultado de 'NotFound' se for o caso.
        /// Caso contrário, busca o artigo com o id especificado, incluindo a categoria relacionada.
        /// </summary>
        /// <param name="id">O identificador único do artigo cujos detalhes serão exibidos.</param>
        /// <returns>Retorna a View com os detalhes do artigo especificado, ou "NotFound" se o artigo não for encontrado.</returns>
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
        /// <summary>
        /// Método responsável por exibir a View para a criação de um novo artigo.
        /// Verifica se o utilizador está logado e se é um administrador; 
        /// se não for admin, redireciona para a página inicial.
        /// </summary>
        /// <returns>Retorna a View para a criação de um novo artigo, 
        /// ou redireciona para a página de login ou para a página inicial, dependendo do status do utilizador.</returns>
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
        /// <summary>
        /// Método responsável por criar um novo artigo.
        /// Verifica se o utilizador está logado e se é um administrador.
        /// Realiza a validação do preço e salva a imagem associada ao artigo.
        /// </summary>
        /// <param name="artigos">Objeto representando o artigo que será criado, com suas propriedades vinculadas do formulário.</param>
        /// <param name="imageFile"> Arquivo de imagem carregado pelo utilizador para o artigo.</param>
        /// <returns>retorna a View "Index" em caso de sucesso, ou a View "Create" com os erros de validação caso ocorra algum problema.</returns>
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
        /// <summary>
        /// Método responsável por exibir a View para edição de um artigo existente.
        /// Verifica se o utilizador está logado e se é um administrador; 
        /// se não for admin, redireciona para a página inicial.
        /// </summary>
        /// <param name="id">Identificador único do artigo que será editado.</param>
        /// <returns>Retorna a View de edição do artigo se o utilizador for autorizado e o artigo existir,
        /// ou redireciona para a página de login ou inicial caso o utilizador não esteja autenticado ou autorizado.
        /// Retorna NotFound se o ID do artigo for nulo ou o artigo não for encontrado.</returns>
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
        /// <summary>
        /// Método responsável por editar um artigo existente no banco de dados.
        /// Verifica se o utilizador está logado e se é um administrador; 
        /// caso contrário, redireciona para a página inicial ou de login.
        /// </summary>
        /// <param name="id">Identificador único do artigo a ser editado. 
        /// Verifica se o ID do artigo corresponde ao artigo enviado na requisição.</param>
        /// <param name="artigos">Objeto representativo do artigo com os dados atualizados enviados pelo formulário.</param>
        /// <param name="imageFile">Arquivo de imagem opcional enviado pelo usuário para atualizar a imagem do artigo.</param>
        /// <returns>Retorna para a página de índice em caso de sucesso na edição ou 
        /// retorna à View de edição com os dados atuais e possíveis mensagens de erro caso falhe.</returns>
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
                existingArtigo.CategoriaFK = artigos.CategoriaFK;

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
        /// <summary>
        /// Método responsável por exibir a página de confirmação de exclusão de um artigo.
        /// Verifica se o utilizador está logado e se é um administrador; 
        /// caso contrário, redireciona para a página inicial ou de login.
        /// </summary>
        /// <param name="id">Identificador único do artigo a ser excluído. 
        /// Se o ID for nulo ou não for encontrado no banco de dados, retorna um resultado de 'NotFound'.</param>
        /// <returns>Retorna uma View com os detalhes do artigo para confirmação de exclusão, 
        /// ou uma página de erro se o artigo não for encontrado.</returns>
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
        /// <summary>
        /// Método responsável por confirmar a exclusão de um artigo.
        /// Verifica se o utilizador está logado e se é um administrador;
        /// caso contrário, redireciona para a página inicial ou de login.
        /// </summary>
        /// <param name="id"> Identificador único do artigo a ser excluído.</param>
        /// <returns>Redireciona para a página de índice de artigos após a exclusão.
        /// Se o artigo não for encontrado, simplesmente redireciona sem erro.</returns>
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
        /// <summary>
        ///  Verifica se um artigo com o ID especificado existe na base de dados.
        /// </summary>
        /// <param name="id">Identificador único do artigo a ser verificado.</param>
        /// <returns>Retorna true se o artigo com o ID fornecido existir; caso contrário, retorna false.</returns>
        private bool ArtigosExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
