namespace WebApplication_Ejemplo_1.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;   // nunca la contraseña en texto plano
        public string Rol { get; set; } = "Usuario";
    }
}
