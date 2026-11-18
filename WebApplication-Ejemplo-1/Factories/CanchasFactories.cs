using Bogus;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Factories
{
    public static class CanchasFactories
    {
        public static void Seed(BaseDeDatos context)
        {
            context.canchas.RemoveRange(context.canchas);
            context.SaveChanges();

            var faker = new Faker<Cancha>("es")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Descripcion, f => $"Cancha {f.Company.CompanyName()}")
                .RuleFor(c => c.Direccion, f => f.Address.StreetAddress());

            var canchas = faker.Generate(10);
            context.canchas.AddRange(canchas);
            context.SaveChanges();
        }
    }
}
