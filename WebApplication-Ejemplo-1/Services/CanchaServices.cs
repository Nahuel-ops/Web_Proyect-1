//using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.DTOs;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Services
{
    public class CanchaServices:ICancha
    {
        private readonly BaseDeDatos _contex;
        public CanchaServices(BaseDeDatos contex)
        {
            _contex = contex;
        }


        //Agregamos los metodos
        public IEnumerable<Cancha> ObtenerTodos()
        {
            return _contex.canchas.ToList();
        }
        //obtener por id
        public Cancha? ObtenerPorId(Guid id)
        {
            return _contex.canchas.Find(id);

        }
        //obtener crear
        public void Crear(CanchaDTOs canchaDTOs)
        {
            var cancha = new Cancha //instanciar objeto
            {
                Descripcion = canchaDTOs.Descripcion,
                Direccion = canchaDTOs.Direccion,
            };
            _contex.canchas.Add(cancha);
            _contex.SaveChanges();
        }
        

    }
}
