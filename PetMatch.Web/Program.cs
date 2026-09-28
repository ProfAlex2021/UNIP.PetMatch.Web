using Microsoft.AspNetCore.Mvc;
using PetMatch.Web.Data;
using PetMatch.Web.Endpoints;
using PetMatch.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PetMemoryStore>();

var app = builder.Build();

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