using System.ComponentModel.DataAnnotations.Schema;
namespace PetMatch.Web.Models;

public sealed class Pet
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string Porte { get; set; } = string.Empty;
    public int IdadeAproximada { get; set; }
    public string Cidade { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = "🐾";
    public bool Disponivel { get; set; } = true;
    public string CaracteristicasTexto { get; set; } = string.Empty;
    [NotMapped]
    public string[] Caracteristicas
    {
        get => CaracteristicasTexto
                   .Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        set => CaracteristicasTexto = string.Join('|', value);
    }
}