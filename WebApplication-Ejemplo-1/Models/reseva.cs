using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication_Ejemplo_1.Models
{
    public class reseva
    {   

        [Key]
        public int id { get; set; }
        [Required (ErrorMessage = "la fecha es requerida")]
        [DataType(DataType.Date)]

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int IdCancha { get; set; }
        [ForeignKey(nameof(IdCancha))]

        public int IdCliente { get; set; }
        [ForeignKey(nameof(IdCliente))] 
        public virtual Cliente? Cliente { get; set; } = null;

    }
}
