
using Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ReservaCreateDto
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [RangoHorario]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [RangoHorario]
        public TimeSpan HoraFin { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SalonId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClienteId { get; set; }
    }
}
