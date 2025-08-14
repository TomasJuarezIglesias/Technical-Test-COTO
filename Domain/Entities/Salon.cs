
namespace Domain.Entities
{
    public class Salon : Entity
    {
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }

        
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
