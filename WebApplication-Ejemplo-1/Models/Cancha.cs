using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Ejemplo_1.Models
{
    public class Cancha
    {
        [Key]
        //este es de antes de el nuevomodelo
        //public int Id { get; set; }
        //este es el que utiliza el hash
        public Guid Id { get; set; } // esto no se necesita exponer

        [Required(ErrorMessage = "la descripcion es requerida")]
        public string? Descripcion { get; set; } //esto si se pued expoer

        [Required(ErrorMessage = "la direccion es requerida")]
        public string? Direccion { get; set; } //esto si se pued expoer


    }
}
