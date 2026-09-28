using PetMatch.Web.Models;
namespace PetMatch.Web.Data;

public sealed class PetMemoryStore
{
    private readonly List<Pet> _pets =
    [
        new Pet(
            1,
            "Luna",
            "Cachorro",
            "Médio",
            3,
            "Curitiba",
            "Dócil, brincalhona e acostumada com crianças.",
            "🐶",
            true,
            ["Vacinada", "Castrada", "Sociável"]),
        new Pet(
            2,
            "Mingau",
            "Gato",
            "Pequeno",
            2,
            "São Paulo",
            "Calmo, curioso e ideal para apartamento.",
            "🐱",
            true,
            ["Vermifugado", "Independente", "Carinhoso"]),
        new Pet(
            3,
            "Thor",
            "Cachorro",
            "Grande",
            5,
            "Belo Horizonte",
            "Protetor, ativo e indicado para lares com espaço.",
            "🐕",
            true,
            ["Adestrado", "Energético", "Companheiro"]),
        new Pet(
            4,
            "Amora",
            "Gato",
            "Pequeno",
            1,
            "Florianópolis",
            "Filhote afetuosa, resgatada recentemente.",
            "🐈",
            true,
            ["Filhote", "Brincalhona", "Resgatada"])
    ];
    public IReadOnlyList<Pet> Listar(string? especie, string? porte, string? cidade)
    {
        IEnumerable<Pet> consulta = _pets.Where(pet => pet.Disponivel);
        if (!string.IsNullOrWhiteSpace(especie))
        {
            consulta = consulta.Where(pet =>
            pet.Especie.Equals(especie, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(porte))
        {
            consulta = consulta.Where(pet =>
            pet.Porte.Equals(porte, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrWhiteSpace(cidade))
        {
            consulta = consulta.Where(pet =>
            pet.Cidade.Contains(cidade, StringComparison.OrdinalIgnoreCase));
        }
        return consulta.ToList();
    }
    public Pet? ObterPorId(int id)
    {
        return _pets.FirstOrDefault(pet => pet.Id == id);
    }
}