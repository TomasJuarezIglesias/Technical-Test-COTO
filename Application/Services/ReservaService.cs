using Application.Dtos;
using Domain.Entities;
using Domain.IRepository;

namespace Application.Services
{
    public class ReservaService(
        IRepository<Reserva> reservaRepository,
        IRepository<Salon> salonRepository,
        IRepository<Cliente> clienteRepository)
    {

        public async Task<IEnumerable<ReservaDto>> GetByDate(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservaDto> Save(ReservaSaveDto reserva)
        {
            throw new NotImplementedException();
        }

    }
}
