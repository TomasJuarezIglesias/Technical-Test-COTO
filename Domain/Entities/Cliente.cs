namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
