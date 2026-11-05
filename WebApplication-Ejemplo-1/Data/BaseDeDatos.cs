using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Data
{
    public class BaseDeDatos: DbContext
    {
       public BaseDeDatos(DbContextOptions<BaseDeDatos> options) : base(options)
        {
        }
        public DbSet<Cancha> Canchas { get; set; }
    }
}
