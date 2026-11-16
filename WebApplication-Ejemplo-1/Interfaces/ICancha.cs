using WebApplication_Ejemplo_1.DTOs;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Interfaces
{
    public interface ICancha
    {
        IEnumerable<Cancha> ObtenerTodos();
        Cancha ObtenerPorId(Guid id);
        void Crear(CanchaDTOs canchaDtos);

    }
}
