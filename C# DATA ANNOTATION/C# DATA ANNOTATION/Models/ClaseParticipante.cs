using System.ComponentModel.DataAnnotations;

namespace RAIZA.Models
{
    public class ClaseParticipante
    {
        [Required(ErrorMessage = "La clase es obligatoria.")]
        public int idclase { get; set; }

        [Required(ErrorMessage = "El estudiante es obligatorio.")]
        public int idestudiante { get; set; }

        public DateTime? FechaIngreso { get; set; }
    }
}