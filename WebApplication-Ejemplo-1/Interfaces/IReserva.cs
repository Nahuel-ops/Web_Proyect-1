using WebApplication_Ejemplo_1.DTOs;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Interfaces
{
    public interface IReserva
    {
        IEnumerable<reseva> ObtenerTodos();
        reseva? ObtenerPorId(Guid id);
        void Crear(ReservaDTOs reservaDtos);
    }
}
