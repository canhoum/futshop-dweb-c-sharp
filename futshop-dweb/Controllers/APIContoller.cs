using futshop_dweb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Models;
using DW_Final_Project.Models;
using System.Globalization;


public class APIController : Controller
{
    private readonly ApplicationDbContext _context;

    public APIController(ApplicationDbContext context)
    {
        _context = context;
    }

    //login utilizadores
    [HttpPost("/api/login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
        {
            return BadRequest("Invalid client request");
        }

        var user = await _context.Utilizadores
            .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);

        if (user != null)
        {

            return Ok(user);
        }
        else
        {
            return NotFound("nao existe");
        }
    }


    //registo utilizadores
    [HttpPost("/api/register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel utilizador)
    {
        if (string.IsNullOrEmpty(utilizador.Utilizador.Email) || string.IsNullOrEmpty(utilizador.Utilizador.Password) ||
            string.IsNullOrEmpty(utilizador.Utilizador.Nome) || string.IsNullOrEmpty(utilizador.Utilizador.Telemovel) ||
            string.IsNullOrEmpty(utilizador.Utilizador.Cidade) || string.IsNullOrEmpty(utilizador.Utilizador.morada) ||
            string.IsNullOrEmpty(utilizador.Utilizador.codigopostal) || string.IsNullOrEmpty(utilizador.Dataformatada) ||
            string.IsNullOrEmpty(utilizador.Utilizador.Pais))
        {
            return BadRequest("Invalid client request");
        }

        var existingUser = await _context.Utilizadores
            .FirstOrDefaultAsync(u => u.Email == utilizador.Utilizador.Email);

        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }
        DateOnly dataNasc;
        if (!DateOnly.TryParse(utilizador.Dataformatada, out dataNasc))
        {
            return BadRequest("Invalid Date format");
        }
        var newUser = new Utilizador
        {
            Email = utilizador.Utilizador.Email,
            Password = utilizador.Utilizador.Password,
            Nome = utilizador.Utilizador.Nome,
            Telemovel = utilizador.Utilizador.Telemovel,
            DataNascimento = dataNasc,
            morada = utilizador.Utilizador.morada,
            codigopostal = utilizador.Utilizador.codigopostal,
            Cidade = utilizador.Utilizador.Cidade,
            Pais = utilizador.Utilizador.Pais

        };
        try
        {
            _context.Utilizadores.Add(newUser);
            _context.SaveChanges();
            return Ok("User created successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return BadRequest();
    }



    //para ir buscar as infos dos users e apresentar no perfil e na fatura
    [HttpGet("/api/user/{id}")]
    public async Task<IActionResult> Profile(int id)
    {
        if (id == null || id == 0)
        {
            return BadRequest();
        }
        var person = await _context.Utilizadores.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }



    [HttpPost("/api/artigo")]
    public async Task<IActionResult> PostArtigo([FromBody] ArtigoDTO artigoDTO){
        try
        {
            

            // Aqui você pode validar o modelo ArtigoDTO antes de prosseguir, se necessário

            // Convertendo ArtigoDTO para a entidade Artigos
            Artigos artigo = new Artigos
            {
                Nome = artigoDTO.Nome,
                Descricao = artigoDTO.Descricao,
                Tamanho = artigoDTO.Tamanho,
                Quantidade = artigoDTO.Quantidade,
                Preco = artigoDTO.Preco,
                CategoriaFK = artigoDTO.CategoriaFK
            };

            // Salvar artigo no banco de dados (ou qualquer outra lógica necessária)
            _context.Artigos.Add(artigo);
            await _context.SaveChangesAsync();
            return Ok("User created successfully");

        }
        catch (Exception ex)
        {
            // Log de erros ou tratamento de exceções
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }


    [HttpGet("/api/utilizadores")]
    public async Task<IActionResult> GetUtilizadores()
    {
        var utilizadores = await _context.Utilizadores.ToListAsync();
        return Ok(utilizadores);
    }

    [HttpGet("/api/artigos")]
    public async Task<IActionResult> GetArtigos()
    {
        var artigos = await _context.Artigos.ToListAsync();
        return Ok(artigos);
    }
}