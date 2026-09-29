using PetMatch.Web.Models;
namespace PetMatch.Web.Data;

public sealed class PetMatchSeed
{
    private readonly PetMatchDbContext _dbContext;
    public PetMatchSeed(PetMatchDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Seed()
    {
        if (_dbContext.Pets.Any())
        {
            return;
        }
        _dbContext.Pets.AddRange(
            new Pet
            {
                Nome = "Luna",
                Especie = "Cachorro",
                Porte = "Médio",
                IdadeAproximada = 3,
                Cidade = "Curitiba",
                Descricao = "Dócil, brincalhona e acostumada com crianças.",
                Icone = "🐶",
                Disponivel = true,
                Caracteristicas = ["Vacinada", "Castrada", "Sociável"]
            },
            new Pet
            {
                Nome = "Mingau",
                Especie = "Gato",
                Porte = "Pequeno",
                IdadeAproximada = 2,
                Cidade = "São Paulo",
                Descricao = "Calmo, curioso e ideal para apartamento.",
                Icone = "🐱",
                Disponivel = true,
                Caracteristicas = ["Vermifugado", "Independente", "Carinhoso"]
            });
        _dbContext.SaveChanges();
    }
}
