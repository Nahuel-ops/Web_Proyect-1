using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_Ejemplo_1.Data;
using Contrato;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CanchaController : ControllerBase
    {
        //El anterior codigo lo borramos
        //Desde esta parte estamos dando el contro a services y quitando el trabajo a...
        private readonly ICancha _canchaService;

        public CanchaController(ICancha canchaService)
        {
            _canchaService = canchaService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_canchaService.ObtenerTodos());
        }
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cancha = _canchaService.ObtenerPorId(id);
            if (cancha == null)
            {
                return NotFound("el Id no se encuentra en cancha");
            }
            return Ok(cancha);
        }
        [HttpPost]
        public IActionResult Post(CanchaDTOs canchaDto)
        {
            _canchaService.Crear(canchaDto);
            return Ok("Datos resgistrados correctamente");
        }


        // SUBIR IMAGEN AL SERVIDOR: POST api/Cancha/subir-imagen
        [HttpPost("subir-imagen")]
        public async Task<IActionResult> SubirImagen(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se seleccionó ningún archivo.");
            }

            string carpetaDestino = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
            if (!Directory.Exists(carpetaDestino))
            {
                Directory.CreateDirectory(carpetaDestino);
            }

            string nombreUnico = $"{Guid.NewGuid()}_{Path.GetFileName(archivo.FileName)}";
            string rutaCompleta = Path.Combine(carpetaDestino, nombreUnico);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Devuelve la URL pública donde quedó guardada la imagen en el servidor
            string urlPublica = $"{Request.Scheme}://{Request.Host}/imagenes/{nombreUnico}";
            return Ok(new { url = urlPublica });
        }
    }
}
