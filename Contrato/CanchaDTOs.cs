namespace Contrato
{
    public class CanchaDTOs
    {
        public string Descripcion { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        //Agregamos el atributo imagenes
        public string? Imagen { get; set; }
    }
}
