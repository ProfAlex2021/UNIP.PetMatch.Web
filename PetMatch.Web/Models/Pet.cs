namespace PetMatch.Web.Models;

public sealed record Pet(
    int Id,
    string Nome,
    string Especie,
    string Porte,
    int IdadeAproximada,
    string Cidade,
    string Descricao,
    string Icone,
    bool Disponivel,
    string[] Caracteristicas);