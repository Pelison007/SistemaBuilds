using Microsoft.EntityFrameworkCore;
using SistemaBuild.API.Modelo;
namespace SistemaBuild.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Classe> Classes {get; set;}
    public DbSet<Equipamento> Equipamentos {get;set;}
    public DbSet<Personagem> Personagens {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaBuilds.db");
    }
}