using Microsoft.EntityFrameworkCore;
using SistemaBuild.API.Modelo;
namespace SistemaBuild.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Classe> Classes {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=SistemaBuilds.db");
    }
}