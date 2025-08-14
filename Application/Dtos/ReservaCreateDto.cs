
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ReservaCreateDto
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El ID de salón es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de salón debe ser válido.")]
        public int SalonId { get; set; }

        [Required(ErrorMessage = "El ID de Cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de cliente debe ser válido.")]
        public int ClienteId { get; set; }
    }
}
