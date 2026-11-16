using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Factories
{
    public static class CanchaFactories
    {
        public static List<Cancha> CrearCanchas()
        {
            return new List<Cancha>
            {
                new Cancha
                {
                    Id = Guid.NewGuid(),
                    Descripcion = "Cancha Sintética 1 - Fútbol 5",
                    Direccion = "Av. Las Américas #120"
                },
                new Cancha
                {
                    Id = Guid.NewGuid(),
                    Descripcion = "Cancha Sintética 2 - Fútbol 7",
                    Direccion = "Av. Las Américas #125"
                },
                new Cancha
                {
                    Id = Guid.NewGuid(),
                    Descripcion = "Cancha Techada Norte",
                    Direccion = "Calle Colón #450"
                },
                new Cancha
                {
                    Id = Guid.NewGuid(),
                    Descripcion = "Cancha Polifuncional Sur",
                    Direccion = "Barrio San José #89"
                },
                new Cancha
                {
                    Id = Guid.NewGuid(),
                    Descripcion = "Cancha Central Profesional",
                    Direccion = "Av. Víctor Paz #310"
                }
            };
        }
    }
}
