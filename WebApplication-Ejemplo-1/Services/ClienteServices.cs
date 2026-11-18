using WebApplication_Ejemplo_1.Data;
using Contrato;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Services
{
    public class ClienteServices : ICliente
    {
        private readonly BaseDeDatos _contex;

        public ClienteServices(BaseDeDatos contex)
        {
            _contex = contex;
        }

        // Obtener todos los clientes
        public IEnumerable<Cliente> ObtenerTodos()
        {
            return _contex.clientes.ToList();
        }

        // Obtener cliente por Id
        public Cliente? ObtenerPorId(Guid id)
        {
            return _contex.clientes.Find(id);
        }

        // Crear un nuevo cliente desde el DTO
        public void Crear(ClienteDTOs clienteDTOs)
        {
            var cliente = new Cliente
            {
                Nombre = clienteDTOs.Nombre,
                Telefono = clienteDTOs.Telefono
            };

            _contex.clientes.Add(cliente);
            _contex.SaveChanges();
        }
    }
}
