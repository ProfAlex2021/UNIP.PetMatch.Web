namespace PetMatch.Web.Models;

public sealed record AdoptionInterestRequest(
    int PetId,
    string Nome,
    string Email,
    string Mensagem);