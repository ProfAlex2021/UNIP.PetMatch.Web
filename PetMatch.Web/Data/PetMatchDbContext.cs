using Microsoft.EntityFrameworkCore;
using PetMatch.Web.Models;

namespace PetMatch.Web.Data;

public sealed class PetMatchDbContext : DbContext
{
    public PetMatchDbContext(DbContextOptions<PetMatchDbContext> options) : base(options)
    {
    }
    public DbSet<Pet> Pets => Set<Pet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(pet => pet.Id);
            entity.Property(pet => pet.Nome).HasMaxLength(80).IsRequired();
            entity.Property(pet => pet.Especie).HasMaxLength(40).IsRequired();
            entity.Property(pet => pet.Porte).HasMaxLength(30).IsRequired();
            entity.Property(pet => pet.Cidade).HasMaxLength(80).IsRequired();
            entity.Property(pet => pet.Descricao).HasMaxLength(300).IsRequired();
            entity.Property(pet => pet.Icone).HasMaxLength(12).IsRequired();
            entity.Property(pet => pet.Caracteristicas).HasMaxLength(300)
.IsRequired();
        });
    }
}