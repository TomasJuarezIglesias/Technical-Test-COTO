using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.IRepository;

namespace Application.Services
{
    public class ReservaService(
        IRepository<Reserva> reservaRepository,
        IRepository<Salon> salonRepository,
        IRepository<Cliente> clienteRepository,
        IMapper mapper) : IReservaService
    {

        public async Task<ApiResponse<IEnumerable<ReservaDto>>> GetByDate(DateTime fecha)
        {
            var reservas = await reservaRepository.FindAsync(r => r.Fecha.Date == fecha.Date);

            var reservasMapped = mapper.Map<IEnumerable<ReservaDto>>(reservas);

            return ApiResponse<IEnumerable<ReservaDto>>.SuccessResponse(reservasMapped);
        }

        public async Task<ReservaDto> Create(ReservaCreateDto reserva)
        {
            throw new NotImplementedException();
        }

    }
}
