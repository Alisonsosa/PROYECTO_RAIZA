using System.ComponentModel.DataAnnotations;

namespace RAIZA.Models
{
    public class Administrador
    {
        [Key]
        public int idadministrador { get; set; }

        [Required(ErrorMessage = "El nivel de acceso es obligatorio.")]
        [Range(1, 5, ErrorMessage = "El nivel de acceso debe estar entre 1 y 5.")]
        public int NivelAcceso { get; set; }
    }
}