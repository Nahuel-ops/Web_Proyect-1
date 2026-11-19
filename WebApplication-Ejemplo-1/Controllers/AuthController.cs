using Contrato;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Ejemplo_1.Interfaces;

namespace WebApplication_Ejemplo_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authService;

        public AuthController(IAuthInterface authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] LoginDto dto)
        {
            try
            {
                _authService.Registrar(dto);
                return Ok(new { mensaje = "Usuario registrado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var respuesta = _authService.Login(dto);

            if (respuesta == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
            }

            return Ok(respuesta);
        }
    }
}
