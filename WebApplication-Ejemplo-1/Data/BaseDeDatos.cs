using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Data
{
    public class BaseDeDatos: DbContext
    {
       public BaseDeDatos(DbContextOptions<BaseDeDatos> options) : base(options)
        {

        }
       public DbSet<Cancha> canchas { get; set; }
       public DbSet<Cliente> clientes { get; set; }
       public DbSet<reseva> resevas { get; set; }
    }
}
