using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication_Ejemplo_1.Models
{
    public class reseva
    {   

        [Key]
        public Guid id { get; set; }

        [Required (ErrorMessage = "la fecha es requerida")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        //es del anterior
        //public int IdCancha { get; set; }
        //ahora o modificamos
        public Guid IdCancha { get; set; }
        [ForeignKey(nameof(IdCancha))]
        //agregado por la IA
        public virtual Cancha? Cancha { get; set; } = null;

        //es del anterior
        //public int IdCliente { get; set; }
        //ahora lo modificamos
        public Guid IdCliente { get; set; }
        [ForeignKey(nameof(IdCliente))] 
        public virtual Cliente? Cliente { get; set; } = null;

    }
}
