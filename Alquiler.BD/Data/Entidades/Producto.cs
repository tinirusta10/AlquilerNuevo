using System.ComponentModel.DataAnnotations;

namespace Alquiler.BD.Data.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]

        public string NombreProducto { get; set; }



        [Required]

        public int PrecioProducto { get; set; }


        [Required]

        public string DetallesProducto { get; set; }



        public List<Foto> Fotos { get; set; }
    }
}
