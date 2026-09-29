using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMatch.Web.Data;
using PetMatch.Web.Models;
using PetMatch.Web.ViewModels;
namespace PetMatch.Web.Controllers;

public sealed class PetsController : Controller
{
    private readonly PetMatchDbContext _dbContext;
    public PetsController(PetMatchDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var pets = await _dbContext.Pets
                   .Where(pet => pet.Disponivel)
                   .OrderBy(pet => pet.Nome)
                   .ToListAsync();
        return View(pets);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Pet());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pet viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        var pet = new Pet
        {
            Nome = viewModel.Nome.Trim(),
            Especie = viewModel.Especie.Trim(),
            Porte = viewModel.Porte.Trim(),
            IdadeAproximada = viewModel.IdadeAproximada,
            Cidade = viewModel.Cidade.Trim(),
            Descricao = viewModel.Descricao.Trim(),
            Icone = viewModel.Icone.Trim(),
            Disponivel = true,
            Caracteristicas = (viewModel.CaracteristicasTexto ?? string.Empty)
                .Split('|')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray()
        };
        _dbContext.Pets.Add(pet);
        await _dbContext.SaveChangesAsync();
        TempData["MensagemSucesso"] = $"Cadastro de {pet.Nome} criado com sucesso." ;
        return RedirectToAction(nameof(Index));
    }
}