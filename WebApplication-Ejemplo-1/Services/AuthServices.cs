using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Contrato;
using Microsoft.IdentityModel.Tokens;
using WebApplication_Ejemplo_1.Data;
using WebApplication_Ejemplo_1.Interfaces;
using WebApplication_Ejemplo_1.Models;

namespace WebApplication_Ejemplo_1.Services
{
    public class AuthServices : IAuthInterface
    {
        private readonly BaseDeDatos _context;
        private readonly IConfiguration _config;

        public AuthServices(BaseDeDatos context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public void Registrar(LoginDto dto)
        {
            if (_context.Usuarios.Any(u => u.NombreUsuario == dto.NombreUsuario))
            {
                throw new Exception("El nombre de usuario ya está registrado.");
            }

            var nuevoUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                NombreUsuario = dto.NombreUsuario,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = "Usuario"
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
        }

        public LoginRespuestaDTO? Login(LoginDto dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == dto.NombreUsuario);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                return null;
            }

            var token = GenerarToken(usuario);

            return new LoginRespuestaDTO
            {
                Token = token,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol
            };
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
