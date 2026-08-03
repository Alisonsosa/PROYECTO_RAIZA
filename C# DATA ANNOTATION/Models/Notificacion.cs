using System.ComponentModel.DataAnnotations;

namespace RAIZA.Models
{
    public class Notificacion
    {
        [Key]
        public int Idnotificacion { get; set; }

        [Required(ErrorMessage = "El tipo de notificación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de notificación no puede superar los 50 caracteres.")]
        public string tiponotificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        [StringLength(500, ErrorMessage = "El mensaje no puede superar los 500 caracteres.")]
        public string mensaje { get; set; } = string.Empty;

        public bool estadoleido { get; set; }

        [Required(ErrorMessage = "La fecha de envío es obligatoria.")]
        public DateTime fechaenvivo { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un usuario.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un usuario válido.")]
        public int idusuario { get; set; }
    }
}