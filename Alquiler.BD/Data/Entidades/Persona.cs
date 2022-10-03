using System.ComponentModel.DataAnnotations;

namespace Alquiler.BD.Data.Entidades
{
    public class Persona
    {

        [Required]

        //es la id de la persona//

        public int Id { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "El DNI de la persona no debe superar los 8 caracteres")]
        public string DNI { get; set; }


        [Required]
        [MaxLength(25)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(25)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "La Contraseña no debe superar los 25 caracteres")]
        public string Contraseña { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "la contraseña no puede superar los 25 caracteres")]
        public string RepetirContraseña { get; set; }

        [Required]
        [MaxLength(50)]
        public string Mail { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "El Numero de telefono no debe superar los 20 caracteres")]
        public string NumeroTelefono { get; set; }



        [Required(ErrorMessage = "El Tipo de Documento es obligatorio")]
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }

        public int DireccionId { get; set; }
        public Direccion Direccion { get; set; }
    }
}
