using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly BaseDeDatos _context;

        public ClienteController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: api/cliente (Listar Clientes)
        [HttpGet(Name = "listarClientes")]
        public async Task<IActionResult> Get()
        {
            var clientes = await _context.clientes.ToListAsync();
            return Ok(clientes);
        }

        // POST: api/cliente (Registrar Cliente con validación de nombre repetido)
        [HttpPost(Name = "guardarCliente")]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            // Verificar si ya existe un cliente con el mismo nombre
            var existe = await _context.clientes
                .AnyAsync(c => c.Nombre == cliente.Nombre);

            if (existe)
            {
                return BadRequest("Ya existe un cliente con ese nombre.");
            }

            await _context.clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return Ok("Cliente registrado correctamente");
        }
    }





}

