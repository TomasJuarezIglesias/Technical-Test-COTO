using Application.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangoHorarioAttribute : ValidationAttribute
    {
        private static readonly TimeSpan HoraMinima = HorarioConstants.HoraApertura;
        private static readonly TimeSpan HoraMaxima = HorarioConstants.HoraCierre;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not TimeSpan timeSpan)
                return new ValidationResult("El valor debe ser de tipo TimeSpan.");

            if (timeSpan.Seconds != 0)
                return new ValidationResult("El formato debe ser HH:mm (sin segundos). Ejemplo: 10:00");

            if (timeSpan < HoraMinima || timeSpan > HoraMaxima)
                return new ValidationResult($"La hora debe estar entre {HoraMinima:hh\\:mm} y {HoraMaxima:hh\\:mm}.");

            return ValidationResult.Success;
        }
    }
}