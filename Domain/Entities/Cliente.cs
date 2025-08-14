namespace Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nombre { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
