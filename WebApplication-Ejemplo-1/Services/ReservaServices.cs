using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.DTOs;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Services
{
    public class ReservaServices : IReserva
    {
        private readonly BaseDeDatos _contex;

        public ReservaServices(BaseDeDatos contex)
        {
            _contex = contex;
        }

        public IEnumerable<reseva> ObtenerTodos()
        {
            return _contex.resevas
                .Include(r => r.Cancha)
                .Include(r => r.Cliente)
                .ToList();
        }

        public reseva? ObtenerPorId(Guid id)
        {
            return _contex.resevas
                .Include(r => r.Cancha)
                .Include(r => r.Cliente)
                .FirstOrDefault(r => r.id == id);
        }

        public void Crear(ReservaDTOs reservaDTOs)
        {
            var reserva = new reseva
            {
                Fecha = reservaDTOs.Fecha,
                IdCancha = reservaDTOs.IdCancha,
                IdCliente = reservaDTOs.IdCliente
            };

            _contex.resevas.Add(reserva);
            _contex.SaveChanges();
        }
    }
}
