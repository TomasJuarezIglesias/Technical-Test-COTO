using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.IRepository;

namespace Application.Services
{
    public class ReservaService(
        IRepository<Reserva> reservaRepository,
        IRepository<Salon> salonRepository,
        IRepository<Cliente> clienteRepository,
        IMapper mapper)
    {

        public async Task<ApiResponse<IEnumerable<ReservaDto>>> GetByDate(DateTime fecha)
        {
            var reservas = await reservaRepository.FindAsync(r => r.Fecha.Date == fecha.Date);

            var reservasMapped = mapper.Map<IEnumerable<ReservaDto>>(reservas);

            return new ApiResponse<IEnumerable<ReservaDto>>(success: true, data: reservasMapped);
        }

        public async Task<ReservaDto> Save(ReservaSaveDto reserva)
        {
            throw new NotImplementedException();
        }

    }
}
