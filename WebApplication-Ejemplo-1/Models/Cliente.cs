using System.ComponentModel.DataAnnotations;

namespace WebApplication_Ejemplo_1.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(10)]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El telefono es requerido")]
        
        public int Telefono { get; set; }



    }
}
