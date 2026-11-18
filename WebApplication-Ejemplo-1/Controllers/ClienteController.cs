using Microsoft.AspNetCore.Mvc;
using Contrato;
using WebApplication_Ejemplo_1.Interfaces;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // Dando el control a services mediante la interfaz
        private readonly ICliente _clienteService;

        public ClienteController(ICliente clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cliente = _clienteService.ObtenerPorId(id);
            if (cliente == null)
            {
                return NotFound("El Id no se encuentra en cliente");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post(ClienteDTOs clienteDto)
        {
            _clienteService.Crear(clienteDto);
            return Ok("Datos registrados correctamente");
        }
    }
}
