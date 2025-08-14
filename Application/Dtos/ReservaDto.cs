namespace Application.Dtos
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public SalonDto Salon { get; set; }
        public ClienteDto Cliente { get; set; }
    }
}
