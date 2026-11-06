using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CanchaController : ControllerBase
    {
         private readonly BaseDeDatos _context;
         public CanchaController(BaseDeDatos context)
         {
                _context = context;
         }
        [HttpGet(Name = "Listar")]
        public async Task<IActionResult> Get()
        {
            var cancha = await _context.canchas.ToListAsync();
            return Ok(cancha);
        }


        //
        [HttpPost(Name = "guardar")]
        public async Task<ActionResult<Cancha>> Post(Cancha cancha)
        {
            var existe = await _context.canchas
                .AnyAsync(c => c.Descripcion == cancha.Descripcion);

            if (existe)
            {
                return BadRequest("Ya existe una cancha con esa descripción.");
            }

            await _context.canchas.AddAsync(cancha);
            await _context.SaveChangesAsync();

            return Ok(cancha);
        }


    }
    
}
