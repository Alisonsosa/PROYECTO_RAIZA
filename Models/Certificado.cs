using System.ComponentModel.DataAnnotations;

namespace RAIZA.Models
{
    public class Certificado
    {
        [Key]
        public int idcertificado { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria.")]
        public DateTime FechaEmision { get; set; }

        [Required(ErrorMessage = "La URL del PDF es obligatoria.")]
        [StringLength(255, ErrorMessage = "La URL no puede superar los 255 caracteres.")]
        public string UrlPdf { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de verificación es obligatorio.")]
        [StringLength(100, ErrorMessage = "El código de verificación no puede superar los 100 caracteres.")]
        public string CodigoVerificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un estudiante.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estudiante válido.")]
        public int idestudiante { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un módulo.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un módulo válido.")]
        public int idmodulo { get; set; }
    }
}