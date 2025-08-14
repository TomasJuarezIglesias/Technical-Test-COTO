
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Reserva : Entity
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }


        public int SalonId { get; set; }
        public virtual Salon Salon { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
