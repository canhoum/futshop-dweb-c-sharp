﻿using futshop_dweb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


public class APIController : Controller
{
    private readonly ApplicationDbContext _context;

    public APIController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("/api/login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Invalid client request");
        }

        var user = await _context.Utilizadores
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            return Ok("ok");
        }
        else
        {
            return Ok("nao existe");
        }
    }


    
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
        if(!DateOnly.TryParse(utilizador.Dataformatada, out dataNasc))
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
        }catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }



        return BadRequest();
    }
}



