using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Ejemplo_1.Models
{
    public class Cancha
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "la descripcion es requerida")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "la direccion es requerida")]
        public string? Direccion { get; set; }


    }
}
