using Contrato;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Interfaces
{
    public interface ICliente
    {
        IEnumerable<Cliente> ObtenerTodos();
        Cliente? ObtenerPorId(Guid id);
        void Crear(ClienteDTOs clienteDtos);
    }
}
