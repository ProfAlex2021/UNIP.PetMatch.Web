using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMatch.Web.Data;
using PetMatch.Web.Endpoints;
using PetMatch.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PetMemoryStore>();

//Adicionando o banco EF Core InMemory
builder.Services.AddDbContext<PetMatchDbContext>(options =>
    options.UseInMemoryDatabase("PetMatchDb"));

var app = builder.Build();

//Iniciando o banco de dados e populando com dados iniciais
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PetMatchDbContext>();
    //dbContext.Database.Migrate(); Sem banco físico, não é necessário migrar, apenas criar o banco em memória
    var seed = scope.ServiceProvider.GetRequiredService<PetMatchSeed>();
    seed.Seed();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

//app.MapGet("/api/ pets", (
//    PetMemoryStore store,
//    string? especie,
//    string? porte,
//    string? cidade) =>
//        {
//            var pets = store.Listar(especie, porte, cidade);
//            return Results.Ok(pets);
//        });
//app.MapGet("/api/pets/{id:int}", (PetMemoryStore store, int id) =>
//{
//    var pet = store.ObterPorId(id);
//    return pet is null
//        ? Results.NotFound(new { mensagem = "Pet não encontrado." })
//        : Results.Ok(pet);
//});
//app.MapGet("/api/boas‑vindas/{nome}", (string nome) =>
//{
//    var nomeTratado = string.IsNullOrWhiteSpace(nome)
//        ? "visitante"
//        : nome.Trim();

//    return Results.Ok(new
//    {
//        mensagem = $"Olá, {nomeTratado} !O PetMatch ajuda você a encontrar uma adoção responsável."
//    });
//});
//app.MapPost("/api/interesses", (
//    PetMemoryStore store,
//    AdoptionInterestRequest request) =>
//    {
//        if (request.PetId <= 0 ||
//        string.IsNullOrWhiteSpace(request.Nome) ||
//        string.IsNullOrWhiteSpace(request.Email) ||
//        !request.Email.Contains('@') ||
//        string.IsNullOrWhiteSpace(request.Mensagem))
//        {
//            return Results.BadRequest(new
//            {
//                mensagem = "Preencha nome, e‑mail, mensagem e um pet válido."
//            });
//        }
//        var pet = store.ObterPorId(request.PetId);
//        if (pet is null)
//        {
//            return Results.NotFound(new
//            {
//                mensagem = "Não foi possível localizar o pet informado."
//            });
//        }
//        return Results.Ok(new
//        {
//            mensagem = $"Interesse registrado para {pet.Nome}. A equipe responsável poderá avaliar o contato.",
//            pet = pet.Nome,
//            interessado = request.Nome.Trim()
//        });
//});

app.MapPetEndpoints();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action = Index}/{id?}");

app.Run();