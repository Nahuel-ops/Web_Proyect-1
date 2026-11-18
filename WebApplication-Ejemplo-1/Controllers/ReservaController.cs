using Microsoft.AspNetCore.Mvc;
using Contrato;
using WebApplication_Ejemplo_1.Interfaces;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReserva _reservaService;

        public ReservaController(IReserva reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet] //mi peticion de tipo get
        public IActionResult Get()
        {
            return Ok(_reservaService.ObtenerTodos());
        }

        [HttpGet("{id}")] //
        public IActionResult Get(Guid id)
        {
            var reserva = _reservaService.ObtenerPorId(id);
            if (reserva == null)
            {
                return NotFound("El Id no se encuentra en reserva");
            }
            return Ok(reserva);
        }

        [HttpPost]
        public IActionResult Post(ReservaDTOs reservaDto) 
        {
            _reservaService.Crear(reservaDto);
            return Ok("Datos registrados correctamente");
        }
    }
}
