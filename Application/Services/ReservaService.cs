using Application.Constants;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
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

        public async Task<ApiResponse<ReservaDto>> Create(ReservaCreateDto reservaDto)
        {
            if (reservaDto.HoraInicio >= reservaDto.HoraFin) throw new BusinessException("Hora fin debe ser mayor a hora inicio");

            var existsSalonTask = salonRepository.ExistsAsync(s => s.Id == reservaDto.SalonId);
            var existsClienteTask = clienteRepository.ExistsAsync(c => c.Id == reservaDto.ClienteId);

            await Task.WhenAll(existsSalonTask, existsClienteTask);

            if (!existsSalonTask.Result)
                throw new BusinessException("Salón no existe");

            if (!existsClienteTask.Result)
                throw new BusinessException("Cliente no existe");

            var reservasExistentes = await reservaRepository
                .FindAsync(r => r.SalonId == reservaDto.SalonId && r.Fecha == reservaDto.Fecha.Date);

            if (reservasExistentes.Any(r => IsOverlapping(reservaDto, r)))
                throw new BusinessException("Conflicto de horario");

            var reservaEntity = mapper.Map<Reserva>(reservaDto);
            var response = await reservaRepository.AddAsync(reservaEntity);
            var reservaMapped = mapper.Map<ReservaDto>(response);

            return ApiResponse<ReservaDto>.SuccessResponse(reservaMapped);
        }

        private bool IsOverlapping(ReservaCreateDto nueva, Reserva existente)
        {
            var buffer = HorarioConstants.MargenEntreReservas;

            var newStart = nueva.HoraInicio;
            var newEnd = nueva.HoraFin;

            var exStart = existente.HoraInicio;
            var exEnd = existente.HoraFin;

            bool noConflict = exEnd + buffer <= newStart || newEnd + buffer <= exStart;

            return !noConflict;
        }

    }
}
